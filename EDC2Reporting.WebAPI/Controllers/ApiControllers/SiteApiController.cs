using DataServices.Interfaces;
using DataServices.Providers;
using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models.Site;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace EDC2Reporting.WebAPI.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
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

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation($"{nameof(SiteApiController)}.{nameof(Index)} method called!!!");
            RepositoryType _repositoryType = repoOptions.RepositoryType;
            IEnumerable<Site> sites;

            if (_repositoryType == RepositoryType.FromDbRepository)
            {
                repository = new FromDbRepository<Site>(_dbContext);
                sites = repository.GetAll();
            }
            else if (_repositoryType == RepositoryType.FromJsonRepository)
            {
                repository = new FromJsonRepository<Site>("Site.json");
                sites = repository.GetAll();
            }
            else sites = new List<Site>() { new Site() { Name = "rotem" } };
            return Ok(sites);
        }


        [HttpGet("Load")]
        public IActionResult Load()
        {
 
            return new JsonResult(new { IsSuccess = false });
        }

    }

}
