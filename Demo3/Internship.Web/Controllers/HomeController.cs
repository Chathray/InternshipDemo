using AutoMapper;
using Internship.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Internship.Web
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IServiceFactory _serviceFactory;

        public HomeController(ILogger<HomeController> logger, IMapper mapper, IServiceFactory serviceFactory)
        {
            _logger = logger;
            _mapper = mapper;
            _serviceFactory = serviceFactory;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            string[] allow = { "GET", "POST" };
            if (allow.Contains(context.HttpContext.Request.Method))
            {
                ViewBag.id = User.Claims.ElementAt(0).Value;
                ViewBag.email = User.Claims.ElementAt(1).Value;
                ViewBag.fullname = User.Claims.ElementAt(2).Value;
                ViewBag.status = User.Claims.ElementAt(3).Value;
            }
        }

        [HttpGet]
        public IActionResult Index(
            int page = 1, int size = 6, int sort = 1,
            int search_on = 0, string search_string = "",
            int on_passed = 2,
            int date_filter = 0, string start_date = "1970-01-01", string end_date = "2070-01-01")
        {
            ViewData["page-1"] = "active";

            DataSet dtset;

            bool haveFilter = on_passed != 2 || date_filter != 0;

            if (haveFilter)
            {
                _logger.LogInformation($"{page},{size},{sort},{search_on},{search_string},{on_passed},{date_filter},{start_date},{end_date}");
                dtset = _serviceFactory.Intern.GetInternByPage(
                     page, size, sort,
                     search_on, search_string,
                     on_passed,
                     date_filter, start_date, end_date);
            }
            else
                dtset = _serviceFactory.Intern.GetInternByPage(
                     page, size, sort,
                     search_on, search_string);

            var total = Convert.ToInt32(dtset.Tables[1].Rows[0]["FOUND_ROWS"]);
            var pagination = new PaginationLogic(total, page, size);

            var model = new IndexViewModel(pagination, dtset,
                _serviceFactory.Training.GetAll(),
                _serviceFactory.Organization.GetAll(),
                _serviceFactory.Department.GetAll(),
                _serviceFactory.Point.GetAll());

            ViewData["trainings.count"] = model.Trainings.Count;
            ViewData["organizations.count"] = model.Organizations.Count;
            ViewData["departments.count"] = model.Departments.Count;
            ViewData["points.count"] = model.Points.Count;

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel model)
        {
            var intern = _mapper.Map<InternModel>(model);
            intern.Mentor = int.Parse(ViewBag.id);

            try
            {
                _serviceFactory.Intern.Create(intern);
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
                _serviceFactory.Intern.Update(intern);
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

            var model = _serviceFactory.Question.GetAll()
                .OrderBy(o => o.Group)
                .ToList();

            return View(model);
        }



        #region INSERT
        [HttpPost]
        public bool InsertQuestion(QuestionViewModel model)
        {
            var qa = _mapper.Map<QuestionModel>(model);
            return _serviceFactory.Question.Create(qa);
        }

        [HttpPost]
        public bool EvaluateIntern(PointViewModel model)
        {
            var mark = _mapper.Map<PointModel>(model);
            mark.Marker = int.Parse(ViewBag.id);
            return _serviceFactory.Point.EvaluateIntern(mark);
        }

        [HttpPost]
        public bool InsertTraining(TrainingModel model)
        {
            model.CreatedBy = int.Parse(ViewBag.id);
            return _serviceFactory.Training.Create(model);
        }
        #endregion



        #region DELETE
        [HttpPost]
        public bool DeleteQuestion(int id)
        {
            return _serviceFactory.Question.Delete(id);
        }
        [HttpPost]
        public bool DeleteIntern(int id)
        {
            return _serviceFactory.Intern.Delete(id);
        }
        [HttpPost]
        public bool DeletePoint(int id)
        {
            return _serviceFactory.Point.Delete(id);
        }
        [HttpPost]
        public bool DeleteOrganization(int id)
        {
            return _serviceFactory.Organization.Delete(id);
        }
        [HttpPost]
        public bool DeleteDepartment(int id)
        {
            return _serviceFactory.Department.Delete(id);
        }
        [HttpPost]
        public bool DeleteTraining(int id)
        {
            return _serviceFactory.Training.Delete(id);
        }
        #endregion



        #region UPDATE
        [HttpPost]
        public bool SetSharedTraining(int sharedId, int[] depArray)
        {
            bool result = true;
            foreach (var depId in depArray)
            {
                _logger.LogInformation(depId + "\n");
                result = result && _serviceFactory.Department.InsertSharedTraining(sharedId, depId);
            }
            return result;
        }
        [HttpPost]
        public bool UpdatePoint(PointModel model)
        {
            model.Marker = int.Parse(ViewBag.id);
            return _serviceFactory.Point.Update(model);
        }
        [HttpPost]
        public bool UpdateQuestion(QuestionModel model)
        {
            return _serviceFactory.Question.Update(model);
        }
        [HttpPost]
        public bool UpdateDepartment(DepartmentModel model)
        {
            return _serviceFactory.Department.Update(model);
        }
        [HttpPost]
        public bool UpdateOrganization(OrganizationModel model)
        {
            return _serviceFactory.Organization.Update(model);
        }
        [HttpPost]
        public bool UpdateTraining(TrainingModel model)
        {
            model.CreatedBy = int.Parse(ViewBag.id);
            return _serviceFactory.Training.Update(model);
        }
        [HttpPost]
        public bool UploadAvatar(string ImgStr, string ImgName)
        {
            return SaveImage(ImgStr, ImgName);
        }
        #endregion



        #region GET
        [HttpGet("CountByIndex/{stt}")]
        public int CountByIndex(int stt)
        {
            return _serviceFactory.User.CountByIndex(stt);
        }

        [HttpGet("Home/GetInternInfo")]
        [HttpGet("GetInternInfo/{id}")]
        public string GetInternInfo(int id)
        {
            return _serviceFactory.Intern.GetInternInfo(id);
        }

        [HttpGet]
        public string GetInternDetail(int id)
        {
            return _serviceFactory.Intern.GetInternDetail(id);
        }

        [HttpGet]
        public string GetTrainingContent(int id)
        {
            return _serviceFactory.Training.GetTrainingContent(id);
        }

        [HttpGet]
        public string GetPassedCount()
        {
            var passed = _serviceFactory.Point.GetPassedCount();
            var total = _serviceFactory.User.CountByIndex(6);
            return (passed / (float)total).ToString("0%");
        }
        [HttpGet]
        public ActionResult GetJointTrainings(int internId)
        {
            var obj = _serviceFactory.Intern.GetJointTrainings(internId);
            return Ok(obj);
        }
        //_____________________________________________________

        [HttpGet]
        public IActionResult GetPoint(int id)
        {
            return Json(_serviceFactory.Point.GetOne(id));
        }
        [HttpGet]
        public IActionResult GetQuestion(int id)
        {
            return Json(_serviceFactory.Question.GetOne(id));
        }
        //_____________________________________________________

        [HttpGet]
        public IActionResult GetOrganizations()
        {
            return Ok(_serviceFactory.Organization.GetAll());
        }
        [HttpGet]
        public IActionResult GetDepartments()
        {
            return Ok(_serviceFactory.Department.GetAll());
        }
        [HttpGet]
        public IActionResult GetTrainings()
        {
            return Json(_serviceFactory.Training.GetAll());
        }
        #endregion


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public bool SaveImage(string ImgStr, string ImgName)
        {
            string path = Environment.CurrentDirectory + "\\wwwroot\\img\\avatar"; //Path

            //Check if directory exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }

            //set the image path
            string imgPath = Path.Combine(path, ImgName);

            try
            {
                byte[] imageBytes = Convert.FromBase64String(ImgStr);
                System.IO.File.WriteAllBytes(imgPath, imageBytes);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}