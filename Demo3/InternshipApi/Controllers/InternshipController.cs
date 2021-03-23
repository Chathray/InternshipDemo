using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InternshipController : ControllerBase
    {

        private readonly ILogger<InternshipController> _logger;

        public InternshipController(ILogger<InternshipController> logger)
        {
            _logger = logger;
        }


    }
}
