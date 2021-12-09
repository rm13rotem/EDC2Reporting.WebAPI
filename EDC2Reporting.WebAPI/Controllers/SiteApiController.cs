using DataServices.Interfaces;
using DataServices.Providers;
using DataServices.SqlServerRepository;
using MainStaticMaintainableEntities.SiteAssembly;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EDC2Reporting.WebAPI.Controllers
{
    [Route("v1/api/Site")]
    [ApiController]
    public class SiteApiController : ControllerBase
    {
        private readonly ILogger<SiteApiController> _logger;
        private IRepository<Site> siteJsonRepository;
        private IRepository<Site> siteDbRepository;
        private IRepository<Site> inMemoryRepository;
        public SiteApiController(ILogger<SiteApiController> logger, EdcDbContext db)
        {
            _logger = logger;
            siteDbRepository = new FromDbRepository<Site>(db);
            siteJsonRepository = new FromJsonRepository<Site>("S ite.json");
            inMemoryRepository = new BaseInMemoryRepository<Site>(siteJsonRepository);
        }

        public IActionResult Index()
        {
            _logger.LogInformation($"{nameof(SiteApiController)}.Index method called!!!");
            return Ok(siteDbRepository.GetAll());
        }
    }
}
