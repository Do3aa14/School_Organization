using Microsoft.AspNetCore.Identity;
using SchoolOrganization.DataLayer.Contract.Role;
using SchoolOrganization.DataLayer.Persistence.GenericRepository;
using SchoolOrganization.Domain.Context;
using SchoolOrganization.ServiceLayer.Contract.UserServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolOrganization.DataLayer.Persistence.Role
{
    public class RoleRepository : GenericRepository<IdentityRole>, IRoleRepository
    {
        private RoleManager<IdentityRole> _roleManager;
        public RoleRepository(RoleManager<IdentityRole> roleManager,
            ApplicationDbContext _dbContext) : base(_dbContext)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CheckRoleExsitanceAsync(UserDto userDto)
        {
            
            bool adminRoleExists = await _roleManager.RoleExistsAsync(userDto.Role);
            if (!adminRoleExists)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(userDto.Role));
                return result.Succeeded;
            }
            return adminRoleExists;
        }
    }
}
