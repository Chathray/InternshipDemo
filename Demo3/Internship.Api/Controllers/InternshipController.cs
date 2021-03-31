using AutoMapper;
using Internship.Application;
using Internship.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship.Api
{
    //  [Authorize]
    [ApiController]
    [Route("internship")]
    public class InternshipController : ControllerBase
    {
        private readonly ILogger<InternshipController> _logger;
        private readonly IMapper _mapper;
        private readonly IInternService _internService;
        private readonly ITrainingService _trainingService;
        private readonly IEventService _eventService;
        private readonly IDepartmentService _departmentService;
        private readonly IOrganizationService _organizationService;
        private readonly IUserService _userService;

        public InternshipController(ILogger<InternshipController> logger, IInternService internService, ITrainingService trainingService, IDepartmentService departmentService, IOrganizationService organizationService, IMapper mapper, IUserService userService, IEventService eventService)
        {
            _logger = logger;
            _mapper = mapper;
            _internService = internService;
            _trainingService = trainingService;
            _departmentService = departmentService;
            _organizationService = organizationService;
            _userService = userService;
            _eventService = eventService;
        }


        [HttpGet("Get/{id}")]
        public string Get(int id)
        {
            return _internService.GetInternInfo(id);
        }

        [HttpGet("GetPage/{page},{size}")]
        public IActionResult GetPage(int page, int size, string sort = "Index")
        {
            var total = _internService.GetCount();
            var pagination = new PaginationLogic(sort, total, page, size);

            return Ok(
                _internService.GetInternByPage(
                pagination.CurrentPage,
                pagination.PageSize, "Index"));
        }

        [HttpGet("GetDepartments")]
        public  IActionResult GetDepartments()
        {
            var obj =  _departmentService.GetAll();
            return Ok(obj);
        }

        [HttpGet("GetOrganizations")]
        public  IActionResult GetOrganizations()
        {
            var obj =  _organizationService.GetAll();
            return Ok(obj);
        }

        [HttpGet("GetUsers")]
        public  IActionResult GetUsers()
        {
            var obj =  _userService.GetAll();
            return Ok(obj);
        }

        [HttpGet("GetEvents")]
        public string GetEvents()
        {
            return _eventService.GetJson();
        }

        [HttpGet("GetJoined/{internId}")]
        public string GetJoined(int internId)
        {
            var data = _eventService.GetEventsIntern();
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
            return eventsJoined.ToString();
        }

        [HttpGet("GetTrainingOn/{internId}")]
        public IActionResult GetTrainingOn(int internId)
        {
            return Ok(_trainingService.GetTrainingByIntern(internId));
        }

        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] InternModel model)
        {
            model.Mentor = int.Parse(User.Claims.ElementAt(0).Value);

            try
            {
                _internService.InsertIntern(model);
            }
            catch (AppException)
            {
                Response.StatusCode = -1;
            }

            return Ok();
        }

        [HttpPost("InsertEvent")]
        public IActionResult InsertEvent(EventModel model)
        {
            model.CreatedBy = int.Parse(User.Claims.ElementAt(0).Value);
            var ok = _eventService.InsertEvent(model);

            if (!ok) Response.StatusCode = -1;

            return Ok(ok);
        }

        [HttpPut("Update/{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Put))]
        public IActionResult Update([FromBody] InternModel model, int id)
        {
            model.Mentor = int.Parse(User.Claims.ElementAt(0).Value);
            model.InternId = id;

            try
            {
                _logger.LogInformation(DataExtensions.Dump(model));
                _internService.UpdateIntern(model);
            }
            catch (AppException)
            {
                Response.StatusCode = -1;
            }
            return Ok();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var result = _internService.RemoveIntern(id);
            return Ok(result);
        }
    }
}
