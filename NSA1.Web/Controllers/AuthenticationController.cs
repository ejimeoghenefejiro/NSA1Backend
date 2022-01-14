using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NSA1.Core.Data;
using NSA1.Core.Dto.EntityModels;
using NSA1.Web.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<Register> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public AuthenticationController(UserManager<Register> userManager, RoleManager<Role> roleManager, IConfiguration configuration, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid client request");
            }

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return NotFound(new
                {
                    type = "https://NSA1.COM",
                    title = "Not Found",
                    status = 400,
                    message = "login failed",
                });
            }
            var verify = await _context.Users.Where(u => u.Email == model.Username).FirstOrDefaultAsync();
            if (verify == null)
            {
                //WelcomeRequest request = new WelcomeRequest
                //{
                //    ToEmail = user.Email,
                //    UserName = user.Email
                //};
                //Send email to user to notify them if they are the one login from a different device only
                // await _emailSender.DiffDeviceLoginAsync(request);
            }
            var clientId = "";// user.Id;
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                clientId = user.Id;
                var userRoles = await _userManager.GetRolesAsync(user);
                var roles = _roleManager.Roles;
                var finalList = new List<Role>();
                foreach (var userRole in userRoles)
                {
                    foreach (var role in roles)
                    {
                        if (role.Name.Equals(userRole))
                        {
                            finalList.Add(role);
                        }
                    }
                }
                var authClaims = new List<Claim>
                {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                foreach (var role in finalList)
                {
                    var claim = await _roleManager.GetClaimsAsync(role);
                    foreach (var c in claim)
                    {
                        if (!authClaims.Contains(c))
                        {
                            authClaims.Add(c);
                        }
                    }
                }
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(7),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new
                {

                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    // refreshToken = refreshToken,
                    expiration = token.ValidTo,
                    clientId = clientId,
                    Message = "login successful",
                    FirstName = user.FirstName,
                    LastName = user.LastName,

                });

            }
            return NotFound(new
            {

                type = "https://tools.ietf.org/html/rfc7235#section-3.1",
                title = "NOT Found",
                status = 400,
                message = "login failed",
            });

        }
        [HttpPost]
        [Route("registerModels")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterModels model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User already exists!" });
            //var phoneNumberExists = await _userManager.Users.Where(x => x.PhoneNumber == model.PhoneNumber).FirstOrDefaultAsync();
            //if (phoneNumberExists != null)
            //    return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = $"This Phone Number: {model.PhoneNumber} already exists!" });
            var currentDate = DateTime.Now;
            Register user = new Register()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBith = model.DateOfBith,
                IsAModel = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });


            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            //WelcomeRequest request = new WelcomeRequest
            //{
            //    ToEmail = model.Email,
            //    UserName = model.Email
            //};
            //await _emailSender.SendWelcomeEmailAsync(request);
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("RegisterMember")]
        public async Task<IActionResult> RegisterMember([FromBody] RegisterModels model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User already exists!" });
            //var phoneNumberExists = await _userManager.Users.Where(x => x.PhoneNumber == model.PhoneNumber).FirstOrDefaultAsync();
            //if (phoneNumberExists != null)
            //    return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = $"This Phone Number: {model.PhoneNumber} already exists!" });
            var currentDate = DateTime.Now;
            Register user = new Register()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBith = model.DateOfBith,
                IsMember = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });


            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            //WelcomeRequest request = new WelcomeRequest
            //{
            //    ToEmail = model.Email,
            //    UserName = model.Email
            //};
            //await _emailSender.SendWelcomeEmailAsync(request);
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
        [HttpPost]
        [Route("RegisterClub")]
        public async Task<IActionResult> RegisterClub([FromBody] RegisterModels model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User already exists!" });
            //var phoneNumberExists = await _userManager.Users.Where(x => x.PhoneNumber == model.PhoneNumber).FirstOrDefaultAsync();
            //if (phoneNumberExists != null)
            //    return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = $"This Phone Number: {model.PhoneNumber} already exists!" });
            var currentDate = DateTime.Now;
            Register user = new Register()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBith = model.DateOfBith,
                IsClub = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });


            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            //WelcomeRequest request = new WelcomeRequest
            //{
            //    ToEmail = model.Email,
            //    UserName = model.Email
            //};
            //await _emailSender.SendWelcomeEmailAsync(request);
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
    }
}
