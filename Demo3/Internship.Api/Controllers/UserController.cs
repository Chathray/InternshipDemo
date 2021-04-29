using AutoMapper;
using Internship.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Internship.Api
{
    [ApiController]
    [Route("{controller}")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;

        public UserController(IMapper mapper, IUserService userService,
            IOptions<AppSettings> appSettings)
        {
            _mapper = mapper;
            _userService = userService;
            _appSettings = appSettings.Value;
        }


        /// <summary>
        /// Validate user access
        /// </summary>
        /// <returns>Basic user information and access Token</returns>
        [HttpPost("/Authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationModel model)
        {
            var user = _userService.Authenticate(model.Email, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                user.UserId,
                Username = user.FirstName + " " + user.LastName,
                user.FirstName,
                user.LastName,
                tokenString
            });
        }


        /// <summary>
        /// Create a new user in the system
        /// </summary>
        /// <param name="model">Information to register</param>
        /// <response code="200">Successful registration</response>
        /// <response code="400">Register fail</response>          
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost("/Register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            try
            {
                var userModel = _mapper.Map<UserModel>(model);
                // create user
                var done = _userService.Create(userModel);

                if (done)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }


        /// <summary>
        /// Get a list of users
        /// </summary>
        /// <returns>The full list of existing users in the system</returns>
        [Authorize]
        [HttpGet("/UserList")]
        [Produces("application/json")]
        public IList<UserModel> UserList()
        {
            var users = _userService.GetAll();
            return users;
        }
    }
}
