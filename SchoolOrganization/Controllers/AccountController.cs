
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolOrganization.API.Models.LoginModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentittywWithJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;


        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModelDto model)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            // check if the user exists
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            // check if the password is valid
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            #region Token Generation
            // create claims for the user
            var claims = new List<Claim>
             {
              new Claim(ClaimTypes.Name, user.UserName)
             };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // create a signing key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("PreNOQh4e_qnxdNU_dqdHP1p"));

            // create a signing credential
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // create a token
            var token = new JwtSecurityToken(
                issuer: "Organization.com",
                audience: "Organization.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            #endregion

            return Ok(new
            {
                StatusCodeResult = StatusCode(StatusCodes.Status200OK),
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
