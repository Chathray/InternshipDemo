using AutoMapper;
using Internship.Application;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Linq;
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

        [HttpGet]
        public async Task<IActionResult> Authentication()
        {
            await Logout();
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Profile()
        {
            SetSessionInfo();
            int userId = int.Parse(ViewBag.id);

            DataTable user = _userService.GetView(userId);
            var model = DataExtensions.GetItem<ProfileViewModel>(user.Rows[0]);
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Settings()
        {
            SetSessionInfo();
            int userId = int.Parse(ViewBag.id);

            UserModel user = _userService.GetOne(userId);
            SettingsViewModel model = _mapper.Map<SettingsViewModel>(user);
            return View(model);
        }

        private void SetSessionInfo()
        {
            ViewBag.id = User.Claims.ElementAt(0).Value;
            ViewBag.email = User.Claims.ElementAt(1).Value;
            ViewBag.fullname = User.Claims.ElementAt(2).Value;
            ViewBag.status = User.Claims.ElementAt(3).Value;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel model)
        {
            if (!ModelState.IsValid) goto Fail;

            UserModel user = _userService.Authenticate(model.LoginEmail, model.LoginPassword);

            if (user == null) goto Fail;

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Surname, user.FirstName +" " + user.LastName),
                new Claim(ClaimTypes.UserData, user.Status),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = model.Remember,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(15)
                });
            goto Done;

        Done:
            return Redirect("/");
        Fail:
            ModelState.AddModelError("fail", "The username or password is incorrect!");
            return View("Authentication", model);
        }

        [HttpPost]
        public IActionResult Register(UserViewModel model)
        {
            var user = _mapper.Map<UserModel>(model);
            try
            {
                bool result = _userService.InsertUser(user);
                if (result) goto Done;
            }
            catch (AppException ex)
            {
                _logger.LogInformation(ex.Message);
            }
            goto Fail;

        Done:
            ModelState.AddModelError("done", "Registration complete, login now!");
            return View("Authentication");
        Fail:
            ModelState.AddModelError("fail", "Registration failed, please try again!");
            return View("Authentication", model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        [HttpPatch]
        public bool UserUpdateBasic(UserViewModel model)
        {
            var user = _mapper.Map<UserModel>(model);
            return _userService.Update(user);
        }

        [HttpPatch]
        public bool UserUpdatePassword(UserViewModel model)
        {
            var user = _mapper.Map<UserModel>(model);
            return _userService.Update(user);
        }

        [HttpPatch]
        public bool UserUpdateEmail(UserViewModel model)
        {
            var user = _mapper.Map<UserModel>(model);
            return _userService.Update(user);
        }

        [HttpPost]
        public bool UserDelete(int id)
        {
            return _userService.Delete(id);
        }
    }
}