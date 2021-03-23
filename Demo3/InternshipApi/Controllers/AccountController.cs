using System;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using InternshipApi.Services;
using InternshipApi.Models;
using Internship.Data;

namespace InternshipApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/{action}")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly AppSettings _appSettings;

        public AccountController(ILogger<AccountController> logger,
            IMapper mapper, IAccountService accountService,
            IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _mapper = mapper;
            _accountService = accountService;
            _appSettings = appSettings.Value;
        }

        [HttpGet("/startup")]
        [AllowAnonymous]
        public IActionResult Check()
        {
            return Ok(_accountService.GetById(1));
        }


        //----------------------------------------------------------------------------------
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticationModel model)
        {
            var user = _accountService.Authenticate(model.LoginEmail, model.LoginPassword);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = user.UserId,
                Username = user.FirstName + " " + user.LastName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });
        }

        [HttpPost]
        public IActionResult Register(AuthenticationModel model)
        {
            // map model to entity
            var user = _mapper.Map<User>(model);

            try
            {
                // create user
                _accountService.InsertUser(user, model.RegiterPassword);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }      
    }
}
