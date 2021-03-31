using AutoMapper;
using Internship.Application;
using Internship.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Internship.Web
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        private readonly IInternService _internService;
        private readonly ITrainingService _trainingService;
        private readonly IEventService _eventService;
        private readonly IEventTypeService _eventTypeService;
        private readonly IQuestionService _questionService;
        private readonly IDepartmentService _departmentService;
        private readonly IOrganizationService _organizationService;
        private readonly IUserService _userService;
        private readonly IInternshipPointService _pointService;

        public HomeController(ILogger<HomeController> logger, IInternService internService, ITrainingService trainingService, IDepartmentService departmentService, IOrganizationService organizationService, IMapper mapper, IUserService userService, IEventService eventService, IEventTypeService eventTypeService, IQuestionService questionService, IInternshipPointService pointService)
        {
            _logger = logger;
            _mapper = mapper;
            _internService = internService;
            _trainingService = trainingService;
            _departmentService = departmentService;
            _organizationService = organizationService;
            _userService = userService;
            _eventService = eventService;
            _eventTypeService = eventTypeService;
            _questionService = questionService;
            _pointService = pointService;
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

            var total = _internService.GetCountAsync();
            var pagination = new PaginationLogic(sort, total.Result, page, size);

            var m = new IndexViewModel(pagination,
                _internService.GetInternModelList(pagination.CurrentPage, pagination.PageSize, sort, search_on, search_string),
                _trainingService.GetAllAsync().Result,
                _organizationService.GetAllAsync().Result,
                _departmentService.GetAllAsync().Result,
                _pointService.GetAllAsync().Result);

            ViewData["tracount"] = m.Trainings.Count;
            ViewData["orgcount"] = m.Organizations.Count;
            ViewData["depcount"] = m.Departments.Count;
            ViewData["poicount"] = m.InternshipPoints.Count;

            return View(m);
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel model)
        {
            var intern = _mapper.Map<InternModel>(model);
            intern.Mentor = int.Parse(ViewBag.id);

            try
            {
                _internService.InsertIntern(intern);
            }
            catch (AppException)
            {
                Response.StatusCode = -1;
            }

            return Redirect("/");
        }

        [HttpPost("InternUpdate/{id}")]
        public IActionResult Index(IndexViewModel model, int id)
        {
            var intern = _mapper.Map<InternModel>(model);
            intern.Mentor = int.Parse(ViewBag.id);
            intern.InternId = id;

            try
            {
                _logger.LogInformation(Dump(intern));
                _internService.UpdateIntern(intern);
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

            var model = _questionService.GetAllAsync();

            return View(model);
        }

        [AcceptVerbs("GET")]
        public IActionResult Calendar()
        {
            ViewData["page-2"] = "active";


            var guests = _internService.GetAllAsync().Result;
            var eventype = _eventTypeService.GetAllAsync().Result;

            var model = new EventViewModel(eventype, guests)
            {
                Creator = ViewBag.fullname
            };

            return View(model);
        }

        [HttpPost]
        public bool InternLeave(int id)
        {
            return _internService.RemoveIntern(id);
        }

        [AcceptVerbs("POST")]
        public IActionResult CreateEvent(EventViewModel model)
        {
            var even = _mapper.Map<EventModel>(model);
            even.CreatedBy = int.Parse(ViewBag.id);

            var ok = _eventService.InsertEvent(even);

            if (!ok) Response.StatusCode = -1;

            return RedirectToAction("Calendar");
        }

        [AllowAnonymous]
        [HttpPost]
        public string GetEvents()
        {
            return _eventService.GetJson();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetOrganizations()
        {
            return Ok(_organizationService.GetAllAsync());
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetDepartments()
        {
            return Ok(_departmentService.GetAllAsync());
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetInternshipPoints()
        {
            return Ok(_pointService.GetAllAsync());
        }

        [AllowAnonymous]
        [HttpPost("Home/GetInternInfo")]
        [HttpPost("GetInternInfo/{id}")]
        public string GetInternInfo(int id)
        {
            return _internService.GetInternInfo(id);
        }

        [AllowAnonymous]
        [HttpPost("Home/GetInternDetail")]
        public string GetInternDetail(int id)
        {
            return _internService.GetInternDetail(id);
        }

        [AllowAnonymous]
        [HttpPost]
        public string GetInternJoined(int internId)
        {
            var data = _eventService.GetEventsIntern();
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