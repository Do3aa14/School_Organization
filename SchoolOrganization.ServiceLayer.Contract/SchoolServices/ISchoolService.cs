using SchoolOrganization.ServiceLayer.Contract.SchoolServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolOrganization.ServiceLayer.Contract.SchoolServices
{
   public interface ISchoolService
    {

        bool AddSchool(SchoolDto schoolDto);

        bool EditSchool(int schoolId ,SchoolDto schoolDto);

        bool DeleteSchool(int schoolId);
    }
}
