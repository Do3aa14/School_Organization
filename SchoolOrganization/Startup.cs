using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolOrganization.API.AppSetting;
using SchoolOrganization.API.Models;
using SchoolOrganization.DataLayer.Contract.Role;
using SchoolOrganization.DataLayer.Contract.School;
using SchoolOrganization.DataLayer.Contract.User;
using SchoolOrganization.DataLayer.Persistence.Role;
using SchoolOrganization.DataLayer.Persistence.School;
using SchoolOrganization.DataLayer.Persistence.User;
using SchoolOrganization.Domain.Context;
using SchoolOrganization.ServiceLayer.Contract.SchoolServices;
using SchoolOrganization.ServiceLayer.Contract.UserServices;
using SchoolOrganization.ServiceLayer.Persistence;
using SchoolOrganization.ServiceLayer.Persistence.SchoolServices;
using SchoolOrganization.ServiceLayer.Persistence.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolOrganization
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            #region Swagger 
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo() { Title = "SchoolOrganization", Version = "v1" });
                config.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter",
                });
                config.AddSecurityRequirement(new OpenApiSecurityRequirement()
                                                    {
                                                        {
                                                            new OpenApiSecurityScheme
                                                            {
                                                                Reference = new OpenApiReference
                                                                {
                                                                    Type = ReferenceType.SecurityScheme,
                                                                    Id = "Bearer"
                                                                },
                                                                Scheme = "oauth2",
                                                                Name = "Bearer",
                                                                In = ParameterLocation.Header,

                                                            },
                                                            new List<string>()
                                                        }
                                                    });
            });
            //services.AddSwaggerGen(swagger =>
            //{
            //    //This is to generate the Default UI of Swagger Documentation
            //    //swagger.SwaggerDoc("v1", new OpenApiInfo
            //    //{
            //    //    Version = "v1",
            //    //    Title = "SchoolOrganization.API",
            //    //    Description = "Authentication and Authorization in ASP.NET 5 with JWT and Swagger"
            //    //});
            //    // To Enable authorization using Swagger (JWT)
            //    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            //    {
            //        Name = "Authorization",
            //        Type = SecuritySchemeType.ApiKey,
            //        Scheme = "Bearer",
            //        BearerFormat = "JWT",
            //        In = ParameterLocation.Header,
            //        Description = "Enter",
            //    });
            //    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement()
            //                                        {
            //                                            {
            //                                                new OpenApiSecurityScheme
            //                                                {
            //                                                    Reference = new OpenApiReference
            //                                                    {
            //                                                        Type = ReferenceType.SecurityScheme,
            //                                                        Id = "Bearer"
            //                                                    },
            //                                                    Scheme = "oauth2",
            //                                                    Name = "Bearer",
            //                                                    In = ParameterLocation.Header,

            //                                                },
            //                                                new List<string>()
            //                                            }
            //                                        });
            //});

            #endregion

            #region DbContext

            services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
            #endregion

            #region JWT 
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization();
            #endregion

            //Get app settings      
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            #region  Services Injection

            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();


            
            #endregion

            #region Auto Mapper Configurations

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ApiMapperProfile());
                mc.AddProfile(new DbMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(config =>
                {
                    config.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment Card Info API");
                });
            }
            InitializeDatabase(app);
            Seed(app).Wait();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
            }
        }

        async Task Seed(IApplicationBuilder app)
        {
            // ...
            // Initiate RoleManager
            var serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Initiate UserManager
            var _userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            bool adminRoleExists = await _roleManager.RoleExistsAsync("Admin");
            if (!adminRoleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            bool SuperadminRoleExists = await _roleManager.RoleExistsAsync("SuperAdmin");
            if (!adminRoleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }

            IdentityUser AminUser = new IdentityUser
            {
                UserName = "Admin@organization.com",
                Email = "Admin@organization.com"
            };
            IdentityUser SuperAminUser = new IdentityUser
            {
                UserName = "SuperAdmin@organization.com",
                Email = "SuperAdmin@organization.com"
            };
            var IsAdminUserExists = await _userManager.FindByNameAsync(AminUser.UserName);
            if (IsAdminUserExists == null)
            {
                await _userManager.CreateAsync(AminUser, "Test@123");
                await _userManager.AddToRoleAsync(AminUser, "Admin");
            }
            var IsSuperAdminUserExists = await _userManager.FindByNameAsync(SuperAminUser.UserName);

            if (IsSuperAdminUserExists == null)
            {
                await _userManager.CreateAsync(SuperAminUser, "Test@123");
                await _userManager.AddToRoleAsync(SuperAminUser, "SuperAdmin");
            }

        }
    }
}
