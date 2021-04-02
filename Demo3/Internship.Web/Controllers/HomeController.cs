using AutoMapper;
using Internship.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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
        private readonly IQuestionService _questionService;
        private readonly IDepartmentService _departmentService;
        private readonly IOrganizationService _organizationService;
        private readonly IUserService _userService;
        private readonly IPointService _pointService;

        public HomeController(ILogger<HomeController> logger, IInternService internService, ITrainingService trainingService, IDepartmentService departmentService, IOrganizationService organizationService, IMapper mapper, IUserService userService, IQuestionService questionService, IPointService pointService)
        {
            _logger = logger;
            _mapper = mapper;
            _internService = internService;
            _trainingService = trainingService;
            _departmentService = departmentService;
            _organizationService = organizationService;
            _userService = userService;
            _questionService = questionService;
            _pointService = pointService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            ViewBag.id = User.Claims.ElementAt(0).Value;
            ViewBag.email = User.Claims.ElementAt(1).Value;
            ViewBag.fullname = User.Claims.ElementAt(2).Value;
            ViewBag.status = User.Claims.ElementAt(3).Value;
        }

        [HttpGet]
        public IActionResult Index(int page, int size, int sort = 1, int search_on = 0, string search_string = "")
        {
            ViewData["page-1"] = "active";

            var total = _userService.CountByIndex(4);
            var pagination = new PaginationLogic(sort, total, page, size);

            var m = new IndexViewModel(pagination,
                _internService.GetInternByPage(pagination.CurrentPage, pagination.PageSize, sort, search_on, search_string),
                _trainingService.GetAll(),
                _organizationService.GetAll(),
                _departmentService.GetAll(),
                _pointService.GetAll());

            ViewData["trainings.count"] = m.Trainings.Count;
            ViewData["organizations.count"] = m.Organizations.Count;
            ViewData["departments.count"] = m.Departments.Count;
            ViewData["points.count"] = m.Points.Count;

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
                _logger.LogInformation(DataExtensions.Dump(intern));
                _internService.UpdateIntern(intern);
            }
            catch (AppException)
            {
                Response.StatusCode = -1;
            }

            return Redirect("/");
        }

        [HttpGet("/Contact")]
        public IActionResult Contact()
        {
            ViewData["page-4"] = "active";

            return View();
        }

        [HttpGet("/Question")]
        public IActionResult Question()
        {
            ViewData["page-3"] = "active";

            var model = _questionService.GetAll();

            return View(model);
        }


        #region INSERT
        [HttpPost]
        public bool EvaluateIntern(PointViewModel model)
        {
            var mark = _mapper.Map<PointModel>(model);
            mark.Marker = int.Parse(ViewBag.id);
            return _pointService.EvaluateIntern(mark);
        }

        [HttpPost]
        public bool InsertTraining(TrainingModel model)
        {
            model.CreatedBy = int.Parse(ViewBag.id);
            return _trainingService.InsertTraining(model);
        }
        #endregion


        #region DELETE
        [HttpPost]
        public bool DeleteIntern(int id)
        {
            return _internService.DeleteIntern(id);
        }
        [HttpPost]
        public bool DeletePoint(int id)
        {
            return _pointService.DeletePoint(id);
        }
        [HttpPost]
        public bool DeleteOrganization(int id)
        {
            return _organizationService.DeleteOrganization(id);
        }
        [HttpPost]
        public bool DeleteDepartment(int id)
        {
            return _departmentService.DeleteDepartment(id);
        }
        #endregion



        #region UPDATE
        [HttpPost]
        public bool UpdatePoint(PointModel model)
        {
            model.Marker = int.Parse(ViewBag.id);
            return _pointService.UpdatePoint(model);
        }
        [HttpPost]
        public bool UpdateDepartment(DepartmentModel model)
        {
            return _departmentService.UpdateDepartment(model);
        }
        [HttpPost]
        public bool UpdateOrganization(OrganizationModel model)
        {
            return _organizationService.UpdateOrganization(model);
        }
        #endregion



        #region GET
        [HttpGet("CountByIndex/{stt}")]
        public int CountByIndex(int stt)
        {
            return _userService.CountByIndex(stt);
        }

        [HttpPost("Home/GetInternInfo")]
        [HttpPost("GetInternInfo/{id}")]
        public string GetInternInfo(int id)
        {
            return _internService.GetInternInfo(id);
        }

        [HttpPost("Home/GetInternDetail")]
        public string GetInternDetail(int id)
        {
            return _internService.GetInternDetail(id);
        }
        //_____________________________________________________
        
        [HttpGet]
        public IActionResult GetPoint(int id)
        {
            return Json(_pointService.GetPoint(id));
        }
        //_____________________________________________________
        
        [HttpGet]
        public IActionResult GetOrganizations()
        {
            return Ok(_organizationService.GetAll());
        }
        [HttpGet]
        public IActionResult GetDepartments()
        {
            return Ok(_departmentService.GetAll());
        }
        [HttpGet]
        public IActionResult GetPoints()
        {
            return Ok(_pointService.GetAllWithName());
        }
        #endregion


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}