using AutoMapper;
using Internship.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Internship.Api
{
    [Authorize]
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
    }
}
