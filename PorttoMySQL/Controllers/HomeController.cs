using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using WebApplication.Helpers;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataAdapter _adapter;
        private readonly IMapper _mapper;

        public HomeController(DataContext context, ILogger<HomeController> logger, IMapper mapper)
        {
            _adapter = new DataAdapter(context);
            _mapper = mapper;
            _logger = logger;
        }

        private void ShiftTopMenuData()
        {
            ViewData["email"] = User.Claims.ElementAt(0).Value;
            ViewData["fullname"] = User.Claims.ElementAt(1).Value;
            ViewData["status"] = User.Claims.ElementAt(2).Value;
            ViewData["id"] = User.Claims.ElementAt(3).Value;
        }

        [HttpGet]
        [Route("/")]
        [Route("/Home")]
        [Route("/Intern/{page}")]
        [Route("/Intern/{page}/{size}")]
        public IActionResult Index(int page, int size)
        {
            ViewBag.page = page;
            ViewBag.size = size;
            
            var total = _adapter.GetInternCount();
                  
            var model = new IndexModel(
                new Pager(total, page, size),
                _adapter.GetInternList(page, size),
                _adapter.GetOrganizations(),
                _adapter.GetDepartments());

            ShiftTopMenuData();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(IndexModel model)
        {
            Intern intern = _mapper.Map<Intern>(model);
            intern.Mentor = int.Parse(User.Claims.ElementAt(3).Value);

            try
            {
                _adapter.CreateIntern(intern);
            }
            catch (AppException)
            {
                Response.StatusCode = -1;
            }

            return Redirect("/");
        }

        [HttpPost]
        public string InternLeave(int id)
        {
            return _adapter.InternLeave(id);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [AcceptVerbs("GET")]
        public IActionResult Question()
        {
            var model = _adapter.GetQuestions();

            ShiftTopMenuData();
            return View(model);
        }

        [AcceptVerbs("GET")]
        public IActionResult Calendar()
        {
            var guests = _adapter.GetInterns();
            var eventype = _adapter.GetEventTypes();

            var model = new CalendarModel(eventype, guests)
            {
                Creator = User.Claims.ElementAt(1).Value
            };

            ShiftTopMenuData();
            return View(model);
        }

        [AcceptVerbs("POST")]
        public IActionResult CreateEvent(CalendarModel model)
        {
            Event even = _mapper.Map<Event>(model);
            even.CreatedBy = int.Parse(User.Claims.ElementAt(3).Value);

            var dat = model.Deadline.Split(" - ");
            // Ngoại lệ ngày đơn
            try
            {
                even.Start = dat[0];
                even.End = dat[1];
            }
            catch { }

            switch (model.Type)
            {
                case "fullcalendar-custom-event-hs-team":
                    even.Type = "Personal";
                    break;
                case "fullcalendar-custom-event-holidays":
                    even.Type = "Holidays";
                    break;
                case "fullcalendar-custom-event-tasks":
                    even.Type = "Tasks";
                    break;
                case "fullcalendar-custom-event-reminders":
                    even.Type = "Reminders";
                    break;
            }
            even.ClassName = model.Type;
            even.GestsField = even.GestsField.ToString()
                .Replace("{", "{{")
                .Replace("}", "}}");

            // Logg to see few word
            _logger.LogInformation(Dump(even));

            _adapter.CreateEvent(even);

            return RedirectToAction("Calendar");
        }

        public static string Dump(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        [AllowAnonymous]
        public IActionResult GetEvents()
        {
            return Ok("Cần tạo json ở đây ở định dạng đúng");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
