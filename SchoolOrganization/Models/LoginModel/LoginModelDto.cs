using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOrganization.API.Models.LoginModel
{
    public class LoginModelDto
    {
        [Required(ErrorMessage = "Username Is Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }
    }
}
