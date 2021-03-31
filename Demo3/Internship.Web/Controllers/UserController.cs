using AutoMapper;
using Internship.Application;
using Internship.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Internship.Web
{
    [Route("/[action]")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IMapper mapper, IUserService users, ILogger<UserController> logger)
        {
            _userService = users;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Authentication()
        {
            await Logout();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel model)
        {
            UserModel user = _userService.Authenticate(model.LoginEmail, model.LoginPassword);

            if (user == null)
            {
                ModelState.AddModelError("result", "Tên người dùng hoặc mật khẩu không chính xác.");
                return View("Authentication", model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FullName", user.FirstName +" " + user.LastName),
                new Claim("Status", user.Status),
                new Claim("ID", user.UserId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    // for test long time
                    ExpiresUtc = DateTime.UtcNow.AddDays(10)
                });

            return Redirect("/");
        }

        [HttpPost]
        public IActionResult Register(UserViewModel model)
        {
            var user = _mapper.Map<UserModel>(model);
            try
            {
                _userService.InsertUser(user);
            }
            catch (AppException ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return RedirectToAction("Authentication");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}