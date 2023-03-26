using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolOrganization.DataLayer.Contract.Role;
using SchoolOrganization.DataLayer.Contract.User;
using SchoolOrganization.Domain.Entities;
using SchoolOrganization.ServiceLayer.Contract.UserServices;
using SchoolOrganization.ServiceLayer.Contract.UserServices.Dto;
using SchoolOrganization.ServiceLayer.Persistence.GenericServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SchoolOrganization.ServiceLayer.Persistence.UserServices
{
    public class UserService : GenericService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private UserManager<IdentityUser> _userManager;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository,
            UserManager<IdentityUser> userManager,
            IMapper mapper, ILogger<UserService> logger) : base(logger, mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userManager = userManager;

        }

        /// <summary>
        ///1- add the user to the database
        ///2- check Role of added user existance and add role if not exist
        ///3- Inser added user to the specified role
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public async Task<IdentityResult> AddUser(UserDto userDto)
        {
            IdentityResult result = new IdentityResult();

            try
            {
                // mapping object 
                IdentityUser user = new IdentityUser
                {
                    UserName = userDto.Username,
                    Email = userDto.Email,
                };

                IdentityResult insertUserResult = await _userRepository.AddUser(user, userDto.Password);

                if (!insertUserResult.Succeeded)
                {
                    return insertUserResult;
                }

                bool roleExist = _roleRepository.CheckRoleExsitanceAsync(userDto).Result;

                if (roleExist)
                {
                    result = await _userRepository.InsertRoleToUser(user, userDto.Role);

                }

            }

            catch (Exception ex)
            {
                _logger.LogError($"UserService>> AddUser>> throws the following Exception {ex.Message}");

            }
            return result;
        }

        /// <summary>
        /// Edit data of user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public async Task<bool> EditUser(string userId, UserDto userDto)
        {
            bool result = false;
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    user.UserName = userDto.Username;
                    user.Email = userDto.Email;
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userDto.Password);

                    result = await _userRepository.EditUser(userId, user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserService>> EditUser>> throws the following Exception {ex.Message}");
            }
            return result;
        }

        /// <summary>
        /// Get All users of database
        /// </summary>
        /// <returns></returns>
        public async Task<List<IdentityUser>> GetAllUsers()
        {
            var users = new List<IdentityUser>();
            try
            {
                users = await _userManager.Users.ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"UserService>> GetAllUsers>> throws the following Exception {ex.Message}");

            }
            return users;
        }

    }
}

