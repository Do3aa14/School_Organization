using Microsoft.AspNetCore.Identity;
using SchoolOrganization.ServiceLayer.Contract.UserServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolOrganization.ServiceLayer.Contract.UserServices
{
 public interface IUserService
    {
        Task<IdentityResult> AddUser(UserDto userDto);
        Task<bool> EditUser(string userId, UserDto user);
        Task<List<IdentityUser>> GetAllUsers();

    }

}
