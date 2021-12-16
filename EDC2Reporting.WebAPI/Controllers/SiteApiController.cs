using DataServices.Interfaces;
using DataServices.Providers;
using DataServices.SqlServerRepository;
using MainStaticMaintainableEntities.SiteAssembly;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace EDC2Reporting.WebAPI.Controllers
{
    public class SiteApiController : ControllerBase
    {
        private readonly ILogger<SiteApiController> _logger;
        private readonly EdcDbContext _dbContext;
        private IRepository<Site> repository;
        private readonly RepositoryOptions repoOptions;

        public SiteApiController(ILogger<SiteApiController> logger, EdcDbContext db, IOptionsSnapshot<RepositoryOptions> options)
        {
            _logger = logger;
            _dbContext = db;
            repoOptions = options.Value;
        }

        public IActionResult Index()
        {
            _logger.LogInformation($"{nameof(SiteApiController)}.Index method called!!!");
            RepositoryType _repositoryType = repoOptions.RepositoryType;
            IEnumerable<Site> sites;

            if (_repositoryType == RepositoryType.FromDbRepository)
            {
                repository = new FromDbRepository<Site>(_dbContext);
                sites = repository.GetAll();
            }
            else            if (_repositoryType == RepositoryType.FromJsonRepository)
            {
                repository = new FromJsonRepository<Site>("Site.json");
                sites = repository.GetAll();                
            }
            else sites = new List<Site>() { new Site() { Name = "rotem" } };
            return Ok(sites);
        }

        public IActionResult OverwriteOtherRepositories(bool forceReloadFromOriginalRepository)
        {
            bool isOverwritingOtherRepositories = repoOptions.IsOverwritingOtherRepositories;
            if (!isOverwritingOtherRepositories)
                return new JsonResult(new { IsSuccess = false });

            if (repoOptions.RepositoryType == RepositoryType.FromDbRepository)
            {
                if (forceReloadFromOriginalRepository)
                    repository = new FromDbRepository<Site>(_dbContext);

                var siteJsonRepository = new FromJsonRepository<Site>(repository, "Site.json");
                return new JsonResult(new { IsSuccess = true });
            }

            if (repoOptions.RepositoryType == RepositoryType.FromJsonRepository)
            {
                if (forceReloadFromOriginalRepository)
                    repository = new FromJsonRepository<Site>("Site.json");

                var _dbRepo = new FromDbRepository<Site>(_dbContext);
                foreach (var site in repository.GetAll())
                {
                    _dbRepo.InsertUpdateOrUndelete(site);
                }
                return new JsonResult(new { IsSuccess = true });
            }

            return new JsonResult(new { IsSuccess = false });
        }

    }

}
