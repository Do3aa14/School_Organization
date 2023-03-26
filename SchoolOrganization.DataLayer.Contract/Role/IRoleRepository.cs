using SchoolOrganization.ServiceLayer.Contract.UserServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolOrganization.DataLayer.Contract.Role
{
   public interface IRoleRepository
    {
        Task<bool> CheckRoleExsitanceAsync(UserDto userDto);
    }
}
