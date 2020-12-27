using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EDC2Reporting.WebAPI.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class SiteApiController : ControllerBase
    {
        private readonly ILogger<SiteApiController> _logger;

        public SiteApiController(ILogger<SiteApiController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("HomeController.Index method called!!!");
            return Ok("Index string");
        }
    }
}
