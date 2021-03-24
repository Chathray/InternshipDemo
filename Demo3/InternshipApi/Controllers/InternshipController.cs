using AutoMapper;
using Internship.Data;
using InternshipApi.Models;
using InternshipApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipApi.Controllers
{
  //  [Authorize]
    [ApiController]
    internal class InternshipController : ControllerBase
    {
        private readonly ILogger<InternshipController> _logger;
        private readonly IMapper _mapper;
        private readonly IInternService _internService;
        private readonly ITrainingService _trainingService;
        private readonly IDepartmentService _departmentService;
        private readonly IOrganizationService _organizationService;

        public InternshipController(ILogger<InternshipController> logger, IInternService internService, ITrainingService trainingService, IDepartmentService departmentService, IOrganizationService organizationService, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _internService = internService;
            _trainingService = trainingService;
            _departmentService = departmentService;
            _organizationService = organizationService;
        }

        [HttpGet]
        public IActionResult FullList(int page, int size, string sort = "Index")
        {
            var total = _internService.GetCountAsync();
            var pagination = new PaginationLogic(sort, total.Result, page, size);

            var m = new IndexModel(pagination,
                _internService.GetInternByPage(pagination.CurrentPage, pagination.PageSize, sort),
                _trainingService.GetTrainings(),
                _organizationService.GetOrganizations(),
                _departmentService.GetDepartments());

            return Ok(m);
        }

        [HttpPost]
        public IActionResult Index(IndexModel model)
        {
            Intern intern = _mapper.Map<Intern>(model);
            intern.Mentor = int.Parse(User.Claims.ElementAt(3).Value);

            try
            {
                _internService.InsertIntern(intern);
            }
            catch (AppException)
            {
                Response.StatusCode = -1;
            }

            return Ok();
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
                _internService.UpdateIntern(intern);
            }
            catch (AppException)
            {
                Response.StatusCode = -1;
            }

            return Redirect("/");
        }


        [HttpPost]
        public IActionResult InternLeave(int id)
        {
            var result = _internService.RemoveIntern(id);
            return Ok(result);
        }

        [HttpPost]
        public string GetInternInfo(int id)
        {
            return _internService.GetInternInfo(id);
        }

        [HttpPost]
        public string GetInternData(int trainingId, int internId)
        {
            var data = _trainingService.GetEventsIntern();
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
{_trainingService.GetTrainingByIntern(trainingId)}
--------------------------------------------------------
- List of training events participating:
{eventsJoined}";
        }


        public static string Dump(object anObject)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(anObject);
        }
    }
}
