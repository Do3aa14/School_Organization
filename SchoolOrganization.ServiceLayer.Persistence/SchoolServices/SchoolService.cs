using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolOrganization.DataLayer.Contract.School;
using SchoolOrganization.Domain.Entities;
using SchoolOrganization.ServiceLayer.Contract.SchoolServices;
using SchoolOrganization.ServiceLayer.Contract.SchoolServices.Dtos;
using SchoolOrganization.ServiceLayer.Persistence.GenericServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolOrganization.ServiceLayer.Persistence.SchoolServices
{
    public class SchoolService : GenericService, ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;
        public SchoolService(ISchoolRepository schoolRepository,
            IMapper mapper, ILogger<SchoolService> logger) : base(logger, mapper)
        {
            _schoolRepository = schoolRepository;
        }
        /// <summary>
        /// Insert school data to database
        /// </summary>
        /// <param name="schoolDto"></param>
        /// <returns></returns>
        public bool AddSchool(SchoolDto schoolDto)
        {
            bool result = false;
            try
            {
                result = _schoolRepository.AddSchool(_mapper.Map<School>(schoolDto));
            }
            catch (Exception ex)
            {
                _logger.LogError($"SchoolService>> AddSchool>> throws the following Exception {ex.Message}");

            }
            return result;
        }

        /// <summary>
        /// Edit data of the specified school
        /// </summary>
        /// <param name="schoolId"></param>
        /// <param name="schoolDto"></param>
        /// <returns></returns>
        public bool EditSchool(int schoolId, SchoolDto schoolDto)
        {
            bool result = false;
            try
            {
                var school = _schoolRepository.GetById(schoolId);
                if (school != null)
                {
                
                    school.School_Id = schoolId;
                    school.School_Name = schoolDto.School_Name;
                    school.School_Address = schoolDto.School_Address;
                    school.Description = schoolDto.Description;

                    result = _schoolRepository.EditSchool(school);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"SchoolService>> EditSchool>> throws the following Exception {ex.Message}");
            }
            return result;
        }

        /// <summary>
        /// Delete data of the specified school
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public bool DeleteSchool(int schoolId)
        {
            bool result = false;
            try
            {
                result = _schoolRepository.DeleteSchool(schoolId);

            }
            catch (Exception ex)
            {
                _logger.LogError($"SchoolService>> DeleteSchool>> throws the following Exception {ex.Message}");

            }
            return result;
        }
    }
}
