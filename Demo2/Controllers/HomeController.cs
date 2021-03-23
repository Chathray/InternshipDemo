using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
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
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new IndexModel(_adapter.GetInterns());

            ShiftTopMenuData();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(IndexModel model)
        {
            Intern intern = _mapper.Map<Intern>(model);
            intern.DateCreated = DateTime.Now;
            intern.CreatedBy = User.Claims.ElementAt(1).Value;
            try
            {
                _adapter.CreateIntern(intern);
            }
            catch (AppException)
            {
                Response.StatusCode = -1;
            }

            model = new IndexModel(_adapter.GetInterns());

            ShiftTopMenuData();
            return View(model);
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
            even.Image = "../img/mastercard.svg";

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

            _adapter.CreateEvent(even);

            return RedirectToAction("Calendar");
        }

        [AllowAnonymous]
        public IActionResult GetEvents()
        {
            return Json(_adapter.GetEvents());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}