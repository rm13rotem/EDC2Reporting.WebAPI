using DataServices.Interfaces;
using DataServices.Providers;
using MainStaticMaintainableEntities.SiteAssembly;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EDC2Reporting.WebAPI.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class SiteApiController : ControllerBase
    {
        private readonly ILogger<SiteApiController> _logger;
        private IRepository<Site> siteRepository;
        public SiteApiController(ILogger<SiteApiController> logger)
        {
            _logger = logger;
            siteRepository = new FromJsonRepository<Site>("site.json");
            siteRepository = new BaseInMemoryRepository<Site>(siteRepository);
        }

        public IActionResult Index()
        {
            _logger.LogInformation($"{nameof(SiteApiController)}.Index method called!!!");
            return Ok(siteRepository.GetAll());
        }
    }
}
