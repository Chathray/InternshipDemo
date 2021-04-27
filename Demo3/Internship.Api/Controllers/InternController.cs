using AutoMapper;
using Internship.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Data;
using System.Linq;
using System.Text;

namespace Internship.Api
{
    //  [Authorize]
    [ApiController]
    [Route("internship")]
    public class InternController : ControllerBase
    {
        private readonly ILogger<InternController> _logger;
        private readonly IMapper _mapper;
        private readonly IServiceFactory _factory;

        public InternController(ILogger<InternController> logger, IMapper mapper, IServiceFactory factory)
        {
            _logger = logger;
            _mapper = mapper;
            _factory = factory;
        }

        [HttpGet("Get/{id}")]
        public string Get(int id)
        {
            return _factory.Intern.GetInternInfo(id);
        }

        [HttpGet("GetPage/{page},{size}")]
        public IActionResult GetPage(int page, int size, string sort = "Index")
        {
            var total = _factory.User.CountByIndex(4);
            var pagination = new PaginationLogic(sort, total, page, size);

            return Ok(
                _factory.Intern.GetInternByPage(
                pagination.CurrentPage,
                pagination.PageSize, "Index"));
        }

        [HttpGet("GetDepartments")]
        public IActionResult GetDepartments()
        {
            var obj = _factory.Department.GetAll();
            return Ok(obj);
        }

        [HttpGet("GetOrganizations")]
        public IActionResult GetOrganizations()
        {
            var obj = _factory.Organization.GetAll();
            return Ok(obj);
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var obj = _factory.User.GetAll();
            return Ok(obj);
        }

        [HttpGet("GetEvents")]
        public string GetEvents()
        {
            return _factory.Event.GetJson();
        }

        [HttpGet("GetJoined/{internId}")]
        public string GetJoined(int internId)
        {
            var data = _factory.Event.GetEventsIntern();
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
            var result = _factory.Intern.GetTraining(internId);
            return Ok(result);
        }

        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] InternModel model)
        {
            model.MentorId = int.Parse(User.Claims.ElementAt(0).Value);

            try
            {
                _factory.Intern.Create(model);
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
            var ok = _factory.Event.InsertEvent(model);

            if (!ok) Response.StatusCode = -1;

            return Ok(ok);
        }

        [HttpPut("Update/{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Put))]
        public IActionResult Update([FromBody] InternModel model, int id)
        {
            model.MentorId = int.Parse(User.Claims.ElementAt(0).Value);
            model.InternId = id;

            try
            {
                _logger.LogInformation(DataExtensions.Dump(model));
                _factory.Intern.Update(model);
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
            var result = _factory.Intern.Delete(id);
            return Ok(result);
        }
    }
}
