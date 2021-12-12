using DataServices.Interfaces;
using DataServices.Providers;
using DataServices.SqlServerRepository;
using MainStaticMaintainableEntities.SiteAssembly;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace EDC2Reporting.WebAPI.Controllers
{
    [Route("v1/api/Site")]
    [ApiController]
    public class SiteApiController : ControllerBase
    {
        private readonly ILogger<SiteApiController> _logger;
        private readonly EdcDbContext _dbContext;
        private IRepository<Site> repository;
        private readonly IConfiguration configuration;

        public SiteApiController(ILogger<SiteApiController> logger, EdcDbContext db, IConfiguration _configuration)
        {
            _logger = logger;
            _dbContext = db;
            repository = new FromDbRepository<Site>(db); //db by default;
            configuration = _configuration;
        }

        public IActionResult Index()
        {
            _logger.LogInformation($"{nameof(SiteApiController)}.Index method called!!!");
            int _repositoryType = configuration.GetValue<int>("RepositorySettings:RepositoryType");
            IEnumerable<Site> sites;

            if (_repositoryType == (int)RepositoryType.FromDbRepository)
            {
                sites = repository.GetAll();
                bool isOverwritingOtherRepositories = configuration.GetValue<bool>("RepositorySettings:IsOverwritingOtherRepositories");
                if (isOverwritingOtherRepositories)
                {
                    var siteJsonRepository = new FromJsonRepository<Site>(repository, "Site.json");
                    repository = new BaseInMemoryRepository<Site>(repository); // way quicker;
                }
                }
            else
            if (_repositoryType == (int)RepositoryType.FromJsonRepository)
            {
                repository = new FromJsonRepository<Site>("Site.json");
                sites = repository.GetAll();
                bool isOverwritingOtherRepositories = configuration.GetValue<bool>("RepositorySettings:IsOverwritingOtherRepositories");
                if (isOverwritingOtherRepositories)
                {
                    var dbRepository = new FromDbRepository<Site>(_dbContext);
                    foreach (var site in sites)
                    {
                        dbRepository.InsertUpdateOrUndelete(site);
                    }
                    repository = new BaseInMemoryRepository<Site>(repository); // way quicker;
                }
            }
            else sites = new List<Site>() { new Site() { Name = "rotem" } };
            return Ok(sites);

        }
    }
}
