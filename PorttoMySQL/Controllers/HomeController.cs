using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
        public IActionResult Index(int page, int size, string sort = "Date")
        {
            var total = _adapter.GetInternCount();
            var pagination = new PaginationLogic(total, page, size);

            var m = new IndexModel(pagination,
                _adapter.GetInternModelList(pagination.CurrentPage, pagination.PageSize, sort),
                _adapter.GetTrainings(),
                _adapter.GetOrganizations(),
                _adapter.GetDepartments());

            ShiftTopMenuData();
            return View(m);
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
            return _adapter.RemoveIntern(id);
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
            even.GestsField = even.GestsField;

            // Logg to see few word
            _logger.LogInformation(Dump(even));

            var ok = _adapter.CreateEvent(even);

            if (!ok) Response.StatusCode = -1;

            return RedirectToAction("Calendar");
        }

        [AllowAnonymous]
        public string GetEvents()
        {
            return _adapter.GetEventsJson();
        }

        [AllowAnonymous]
        public string GetInternData(int id)
        {
            var data = _adapter.GetEventsIntern();
            var eventsJoined = new StringBuilder();

            foreach (DataRow i in data.Rows)
            {
                var t = i["Joined"].ToString();
                if (t.Contains(id + ""))
                {
                    eventsJoined.Append(i["Title"].ToString() + "\n");
                }
            }

            return $@"
                Training Data:
                {_adapter.GetInternTraining(id)}
                --------------------------------
                List of training events participating:
                {eventsJoined}";
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static string Dump(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
