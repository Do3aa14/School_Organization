﻿
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolOrganization.Domain.Entities;

namespace SchoolOrganization.Domain.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        #region Tables

        public DbSet<School> School { get; set; }
        public virtual DbSet<IdentityUser> User { get; set; }
        public virtual DbSet<IdentityRole> Role { get; set; }
        public virtual DbSet<IdentityUserRole<string>> UserRole { get; set; }
        public virtual DbSet<IdentityRoleClaim<string>> RoleClaim { get; set; }
        public virtual DbSet<IdentityUserClaim<string>> UserClaim { get; set; }
        public virtual DbSet<IdentityUserToken<string>> UserToken { get; set; }
        public virtual DbSet<IdentityUserLogin<string>> UserLogin { get; set; }


        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
      

            modelBuilder.Entity<School>();
            modelBuilder.Entity<IdentityUser>(b =>
            {
                // Primary key
                b.HasKey(u => u.Id);

                // Maps to the AspNetUsers table
                b.ToTable("AspNetUsers");


                // Limit the size of columns to use efficient database types
                b.Property(u => u.UserName).HasMaxLength(256);
                b.Property(u => u.Email).HasMaxLength(256);

                // The relationships between User and other entity types
                // Note that these relationships are configured with no navigation properties

                // Each User can have many UserClaims
                b.HasMany<IdentityUserClaim<string>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

                // Each User can have many UserLogins
                b.HasMany<IdentityUserLogin<string>>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

                // Each User can have many UserTokens
                b.HasMany<IdentityUserToken<string>>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany<IdentityUserRole<string>>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(b =>
            {
                // Primary key
                b.HasKey(uc => uc.Id);

                // Maps to the AspNetUserClaims table
                b.ToTable("AspNetUserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                // Composite primary key consisting of the LoginProvider and the key to use
                // with that provider
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });

                // Limit the size of the composite key columns due to common DB restrictions
                b.Property(l => l.LoginProvider).HasMaxLength(128);
                b.Property(l => l.ProviderKey).HasMaxLength(128);

                // Maps to the AspNetUserLogins table
                b.ToTable("AspNetUserLogins");
            });

            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                // Composite primary key consisting of the UserId, LoginProvider and Name
                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });


                // Maps to the AspNetUserTokens table
                b.ToTable("AspNetUserTokens");
            });

            modelBuilder.Entity<IdentityRole<string>>(b =>
            {
                // Primary key
                b.HasKey(r => r.Id);

                // Index for "normalized" role name to allow efficient lookups
                b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

                // Maps to the AspNetRoles table
                b.ToTable("AspNetRoles");

                // A concurrency token for use with the optimistic concurrency checking
                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                // Limit the size of columns to use efficient database types
                b.Property(u => u.Name).HasMaxLength(256);
                b.Property(u => u.NormalizedName).HasMaxLength(256);

                // The relationships between Role and other entity types
                // Note that these relationships are configured with no navigation properties

                // Each Role can have many entries in the UserRole join table
                b.HasMany<IdentityUserRole<string>>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany<IdentityRoleClaim<string>>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(b =>
            {
                // Primary key
                b.HasKey(rc => rc.Id);

                // Maps to the AspNetRoleClaims table
                b.ToTable("AspNetRoleClaims");
            });

            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                // Primary key
                b.HasKey(r => new { r.UserId, r.RoleId });

                // Maps to the AspNetUserRoles table
                b.ToTable("AspNetUserRoles");
            });
            modelBuilder.Entity<IdentityUser>();

            
            base.OnModelCreating(modelBuilder);
        }
        
    }
}

