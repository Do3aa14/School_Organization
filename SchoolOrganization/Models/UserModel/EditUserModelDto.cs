using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOrganization.API.Models.UserModel
{
    public class EditUserModelDto
    {
        [Required(ErrorMessage = "Username Is Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
