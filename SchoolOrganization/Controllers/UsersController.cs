using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolOrganization.API.Controllers;
using SchoolOrganization.API.Models.UserModel;
using SchoolOrganization.ServiceLayer.Contract.UserServices;
using SchoolOrganization.ServiceLayer.Contract.UserServices.Dto;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace IdentittywWithJWT.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
       
        private readonly IUserService _userService;
        public UsersController(IUserService userService, IMapper mapper) : base(mapper)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("Users")]
        public async Task<ActionResult<List<IdentityUser>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
           
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserModelDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userService.AddUser(_mapper.Map<UserDto>(model));
                    if (result.Succeeded)
                        return Ok(HttpStatusCode.OK);
                    else
                        return BadRequest(result.Errors);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
           

          
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("EditUser/{id}")]
        public async Task<IActionResult> EditUser(string id, EditUserModelDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userService.EditUser(id, _mapper.Map<UserDto>(model));
                    if (result)
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
