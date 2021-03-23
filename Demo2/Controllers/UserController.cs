using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("/[action]")]
    public class UserController : Controller
    {
        private readonly DataAdapter _adapter;
        private readonly IMapper _mapper;

        public UserController(DataContext context, IMapper mapper)
        {
            _adapter = new DataAdapter(context);
            _mapper = mapper;
        }

        public async Task<IActionResult> Authentication()
        {
            await Logout();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthenticationModel model)
        {
            var user = _adapter.UserCheck(model.LoginMail, model.LoginPass);

            if (user == null)
                return View("Authentication", model);

            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("FullName", user.FirstName +" " + user.LastName),
                        new Claim("Status", user.Status)
                    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(10)
                });

            return Redirect("/");
        }

        [HttpPost]
        public IActionResult Register(AuthenticationModel model)
        {
            User user = _mapper.Map<User>(model);
            try
            {
                // create user
                _adapter.UserCreate(user, model.Password);
            }
            catch (AppException)
            { }

            return RedirectToAction("Authentication");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}