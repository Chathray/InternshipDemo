using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

        public HomeController(DataAdapter adapter, ILogger<HomeController> logger, IMapper mapper)
        {
            _adapter = adapter;
            _mapper = mapper;
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            ViewBag.email = User.Claims.ElementAt(0).Value;
            ViewBag.fullname = User.Claims.ElementAt(1).Value;
            ViewBag.status = User.Claims.ElementAt(2).Value;
            ViewBag.id = User.Claims.ElementAt(3).Value;
        }

        [HttpGet]
        public IActionResult Index(int page, int size, int sort = 1, int search_on = 0, string search_string = "")
        {
            ViewData["page-1"] = "active";

            var total = _adapter.GetInternCount();
            var pagination = new PaginationLogic(sort, total, page, size);

            var m = new IndexModel(pagination,
                _adapter.GetInternModelList(pagination.CurrentPage, pagination.PageSize, sort, search_on, search_string),
                _adapter.GetTrainings(),
                _adapter.GetOrganizations(),
                _adapter.GetDepartments(),
                _adapter.GetInternshipPoints());

            ViewData["tracount"] = m.Trainings.Count;
            ViewData["orgcount"] = m.Organizations.Count;
            ViewData["depcount"] = m.Departments.Count;
            ViewData["poicount"] = m.InternshipPoints.Count;

            return View(m);
        }

        [HttpPost]
        public IActionResult Index(IndexModel model)
        {
            Intern intern = _mapper.Map<Intern>(model);
            intern.Mentor = int.Parse(ViewBag.id);

            try
            {
                _adapter.InsertIntern(intern);
            }
            catch (AppException)
            {
                Response.StatusCode = -1;
            }

            return Redirect("/");
        }

        [HttpPost("InternUpdate/{id}")]
        public IActionResult Index(IndexModel model, int id)
        {
            Intern intern = _mapper.Map<Intern>(model);
            intern.Mentor = int.Parse(ViewBag.id);
            intern.InternId = id;

            try
            {
                _logger.LogInformation(Dump(intern));
                _adapter.UpdateIntern(intern);
            }
            catch (AppException)
            {
                Response.StatusCode = -1;
            }

            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Contact()
        {
            ViewData["page-4"] = "active";

            return View();
        }

        [AcceptVerbs("GET")]
        public IActionResult Question()
        {
            ViewData["page-3"] = "active";

            var model = _adapter.GetQuestions();

            return View(model);
        }

        [AcceptVerbs("GET")]
        public IActionResult Calendar()
        {
            ViewData["page-2"] = "active";


            var guests = _adapter.GetInterns();
            var eventype = _adapter.GetEventTypes();

            var model = new CalendarModel(eventype, guests)
            {
                Creator = ViewBag.fullname
            };

            return View(model);
        }

        [HttpPost]
        public string InternLeave(int id)
        {
            return _adapter.RemoveIntern(id);
        }

        [AcceptVerbs("POST")]
        public IActionResult CreateEvent(CalendarModel model)
        {
            Event aEvent = _mapper.Map<Event>(model);
            aEvent.CreatedBy = int.Parse(ViewBag.id);

            var dateArray = model.Deadline.Split(" - ");
            // Ngoại lệ ngày đơn
            try
            {
                aEvent.Start = dateArray[0];
                aEvent.End = dateArray[1];
            }
            catch { }

            switch (model.Type)
            {
                case "fullcalendar-custom-event-hs-team":
                    aEvent.Type = "Personal";
                    break;
                case "fullcalendar-custom-event-holidays":
                    aEvent.Type = "Holidays";
                    break;
                case "fullcalendar-custom-event-tasks":
                    aEvent.Type = "Tasks";
                    break;
                case "fullcalendar-custom-event-reminders":
                    aEvent.Type = "Reminders";
                    break;
            }
            aEvent.ClassName = model.Type;

            // Logg to see few word
            //_logger.LogInformation(Dump(even));

            var ok = _adapter.InsertEvent(aEvent);

            if (!ok) Response.StatusCode = -1;

            return RedirectToAction("Calendar");
        }

        [AllowAnonymous]
        [HttpPost]
        public string GetEvents()
        {
            return _adapter.GetEventsJson();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetOrganizations()
        {
            return Ok(_adapter.GetOrganizations());
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetDepartments()
        {
            return Ok(_adapter.GetDepartments());
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetInternshipPoints()
        {
            return Ok(_adapter.GetInternshipPoints());
        }

        [AllowAnonymous]
        [HttpPost("Home/GetInternInfo")]
        [HttpPost("GetInternInfo/{id}")]
        public string GetInternInfo(int id)
        {
            return _adapter.GetInternInfo(id);
        }

        [AllowAnonymous]
        [HttpPost("Home/GetInternDetail")]
        public string GetInternDetail(int id)
        {
            return _adapter.GetInternDetail(id);
        }

        [AllowAnonymous]
        [HttpPost]
        public string GetInternJoined(int internId)
        {
            var data = _adapter.GetEventsIntern();
            JArray array = new();

            foreach (DataRow i in data.Rows)
            {
                var json = i["Joined"].ToString().Split(',', '[', ']', ' ');

                foreach (var token in json)
                {
                    //_logger.LogInformation(token + ", " + iid);
                    if (token == internId.ToString())
                    {
                        array.Add(new JValue(i["Title"]));
                        break;
                    }
                }
            }
            return array.ToString();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static string Dump(object anObject)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(anObject);
        }
    }
}