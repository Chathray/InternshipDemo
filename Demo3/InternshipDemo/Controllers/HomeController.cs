using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
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

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            if (HttpContext.Request.Method == "GET")
            {
                ViewData["email"] = User.Claims.ElementAt(0).Value;
                ViewData["fullname"] = User.Claims.ElementAt(1).Value;
                ViewData["status"] = User.Claims.ElementAt(2).Value;
                ViewData["id"] = User.Claims.ElementAt(3).Value;
            }
        }

        [HttpGet]
        public IActionResult Index(int page, int size, int sort = 1, int search_on = 0, string search_string = "")
        {
            var total = _adapter.GetInternCount();
            var pagination = new PaginationLogic(sort, total, page, size);

            var m = new IndexModel(pagination,
                _adapter.GetInternModelList(pagination.CurrentPage, pagination.PageSize, sort, search_on,search_string),
                _adapter.GetTrainings(),
                _adapter.GetOrganizations(),
                _adapter.GetDepartments());

            return View(m);
        }

        [HttpPost]
        public IActionResult Index(IndexModel model)
        {
            Intern intern = _mapper.Map<Intern>(model);
            intern.Mentor = int.Parse(User.Claims.ElementAt(3).Value);

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
            intern.Mentor = int.Parse(User.Claims.ElementAt(3).Value);
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
            return View();
        }

        [AcceptVerbs("GET")]
        public IActionResult Question()
        {
            var model = _adapter.GetQuestions();

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
            aEvent.CreatedBy = int.Parse(User.Claims.ElementAt(3).Value);

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
        [HttpPost]
        public string GetInternInfo(int id)
        {
            return _adapter.GetInternInfo(id);
        }

        [AllowAnonymous]
        [HttpPost]
        public string GetInternData(int trainingId, int internId)
        {
            var data = _adapter.GetEventsIntern();
            var eventsJoined = new StringBuilder();

            foreach (DataRow i in data.Rows)
            {
                var json = i["Joined"].ToString().Split(',', '[', ']', ' ');

                foreach (var token in json)
                {
                    //_logger.LogInformation(token + ", " + iid);
                    if (token == internId.ToString())
                    {
                        eventsJoined.Append("+ " + i["Title"].ToString() + "\n");
                        break;
                    }
                }
            }
            return $@"
- Training Data:
{_adapter.GetInternTraining(trainingId)}
--------------------------------------------------------
- List of training events participating:
{eventsJoined}";
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
