using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Internship.Web
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly ILogger<EventController> _logger;
        private readonly IMapper _mapper;

        public EventController(ILogger<EventController> logger, IMapper mapper)
        {
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
    }
}