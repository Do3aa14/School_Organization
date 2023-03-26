using Microsoft.AspNetCore.Identity;
using SchoolOrganization.DataLayer.Contract.User;
using SchoolOrganization.DataLayer.Persistence.GenericRepository;
using SchoolOrganization.Domain.Context;
using SchoolOrganization.ServiceLayer.Contract.UserServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolOrganization.DataLayer.Persistence.User
{
    public class UserRepository : GenericRepository<IdentityUser> , IUserRepository 
    {
        private UserManager<IdentityUser> _userManager;
        public UserRepository(UserManager<IdentityUser> userManager,
            ApplicationDbContext _dbContext) : base(_dbContext)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddUser(IdentityUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<bool> EditUser(string userId, IdentityUser user)
        {

            var result = await _userManager.UpdateAsync(user);
         
            return result.Succeeded;
        }


        public async Task<IdentityResult> InsertRoleToUser(IdentityUser user, string role)
        {
           var result = await _userManager.AddToRoleAsync(user, role);
           return result;
         
        }
    }
}
