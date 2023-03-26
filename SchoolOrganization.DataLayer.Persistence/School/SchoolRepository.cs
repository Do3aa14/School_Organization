using SchoolOrganization.DataLayer.Contract.School;
using SchoolOrganization.DataLayer.Persistence.GenericRepository;
using SchoolOrganization.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolOrganization.DataLayer.Persistence.School
{
    public class SchoolRepository : GenericRepository<Domain.Entities.School>, ISchoolRepository
    {
        public SchoolRepository(ApplicationDbContext _dbContext) : base(_dbContext) 
        {

        }
       
        public bool AddSchool(Domain.Entities.School _school)
        {
           return Insert(_dbContext ,_school);
        }

        public bool EditSchool(Domain.Entities.School _school)
        {
           return Update(_dbContext, _school);
            
        }

        public bool DeleteSchool(int schoolId)
        {
            var school = GetById(_dbContext,schoolId);
            if (school != null)
            {
                return Delete(_dbContext, school);
            }
            return false;
        }

        public Domain.Entities.School GetById(int schoolId)
        {
            return GetById(_dbContext, schoolId);
        }
    }


}
