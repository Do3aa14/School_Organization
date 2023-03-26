using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolOrganization.ServiceLayer.Contract.SchoolServices.Dtos
{
  public class SchoolDto
    {

        public int School_Id { get; set; }
        public string School_Name { get; set; }

        public string School_Address { get; set; }

        public string Description { get; set; }
    }
}
