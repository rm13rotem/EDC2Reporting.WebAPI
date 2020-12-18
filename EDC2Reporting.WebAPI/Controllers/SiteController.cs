using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EDC2Reporting.WebAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class SiteController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
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
            return Enumerable.Range(1, 5).Select(index => new PersistantEntity
            {
                CreateDate = DateTime.Now.AddDays(index),
                Id = rng.Next(-20, 55),
                Name = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
