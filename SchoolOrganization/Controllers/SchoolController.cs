using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SchoolOrganization.API.Models.SchoolModel;
using SchoolOrganization.ServiceLayer.Contract.SchoolServices;
using SchoolOrganization.ServiceLayer.Contract.SchoolServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SchoolOrganization.API.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class SchoolController : BaseController
    {
        private readonly ISchoolService _schoolService;
        public SchoolController(ISchoolService schoolService  ,
            IMapper mapper) : base(mapper) 
        {
            _schoolService = schoolService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddSchool")]
        public IActionResult AddSchool([FromBody] SchoolModelDto schoolModelDto)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (_schoolService.AddSchool(_mapper.Map<SchoolDto>(schoolModelDto)))
                        return Ok(HttpStatusCode.OK);
                    else
                        return Ok(HttpStatusCode.InternalServerError);
                }
                return BadRequest();
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("EditSchool")]

        public IActionResult EditSchool(int schoolId , SchoolModelDto schoolModelDto)
        {

            try
            {
                if (ModelState.IsValid && schoolId > 0)
                {

                    if (_schoolService.EditSchool(schoolId,_mapper.Map<SchoolDto>(schoolModelDto)))
                        return Ok(HttpStatusCode.OK);
                    else
                        return Ok(HttpStatusCode.InternalServerError);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteSchool")]
        public IActionResult DeleteSchool(int schoolId)
        {
            try
            {
                if (schoolId > 0)
                {
                    if (_schoolService.DeleteSchool(schoolId))
                        return Ok(HttpStatusCode.OK);
                    else
                        return Ok(HttpStatusCode.InternalServerError);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
