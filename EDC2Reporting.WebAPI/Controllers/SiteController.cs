using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainStaticMaintainableEntities;
using MainStaticMaintainableEntities.SiteAssembly;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EDC2Reporting.WebAPI.Controllers
{
    [Route("v1/[controller]")]
    public class SiteController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Mount Sinai Hospital", "Hadassa Medical Center", "Sheba Medical Center", "Assuta"
        };

        private readonly ILogger<SiteController> _logger;

        public SiteController(ILogger<SiteController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<PersistantEntity> Get()
        {
            var rng = new Random();
            return Summaries.Select(x => new Site() { Name = x })
            .ToArray();
        }
    }
}
