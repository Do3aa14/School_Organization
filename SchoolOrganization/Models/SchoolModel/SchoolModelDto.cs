using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOrganization.API.Models.SchoolModel
{
    public class SchoolModelDto
    {

      

        [Required(ErrorMessage = "School Name Is Required")]
        public string School_Name { get; set; }

        public string School_Address { get; set; }

        public string Description { get; set; }
    }
}
