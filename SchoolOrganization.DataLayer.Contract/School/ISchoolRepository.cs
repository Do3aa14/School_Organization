using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolOrganization.DataLayer.Contract.School
{
  public interface ISchoolRepository
    {
        bool AddSchool(Domain.Entities.School school);

        bool EditSchool( Domain.Entities.School _school);

        bool DeleteSchool(int schoolId);

        Domain.Entities.School GetById(int schoolId);
    }
}
