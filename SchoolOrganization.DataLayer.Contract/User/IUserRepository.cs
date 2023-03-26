using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolOrganization.DataLayer.Contract.User
{
    public interface IUserRepository
    {

        Task<IdentityResult> AddUser(IdentityUser user , string password);
        Task<bool> EditUser(string userId, IdentityUser user);
        Task<IdentityResult> InsertRoleToUser(IdentityUser user , string role);
       
    }
}
