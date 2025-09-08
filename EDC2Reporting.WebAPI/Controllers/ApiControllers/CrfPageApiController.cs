using DataServices.Interfaces;
using DataServices.Providers;
using DataServices.SqlServerRepository.Models.CrfModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EDC2Reporting.WebAPI.Controllers.ApiControllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class CrfPageApiController : ControllerBase
    {
        private ILogger<CrfPageApiController> logger;
        IRepository<CrfPage> repository;
        RepositoryOptions repositorySettings;

        public CrfPageApiController(ILogger<CrfPageApiController> _logger, 
            IOptionsSnapshot<RepositoryOptions> options)
        {
            logger = _logger;
            repositorySettings = options.Value;
            var locator = RepositoryLocator<CrfPage>.Instance;
            repository = locator.GetRepository(repositorySettings.RepositoryType);
        }

        // GET: api/<CrfPageApiController>
        [HttpGet]
        public IEnumerable<CrfPage> Get()
        {
            return repository.GetAll().Where(x => x.IsDeleted == false).ToList();
        }

        // GET api/<CrfPageApiController>/5
        [HttpGet("{id}")]
        public CrfPage Get(int id)
        {
            return repository.GetById(id);
        }

        // POST api/<CrfPageApiController>
        [HttpPost]
        public void Post([FromBody] CrfPage CrfPage)
        {
            repository.UpsertActivation(CrfPage);
        }

        // PUT api/<CrfPageApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CrfPage m)
        {
            repository.Update(m);
        }

        // DELETE api/<CrfPageApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var entity = repository.GetById(id);
            if (entity != null)
                entity.IsDeleted = true;
            repository.Update(entity);
        }
        
        // UNDELETE api/<CrfPageApiController>/Undelete/5
        public void Undelete(int id)
        {
            var entity = repository.GetById(id);
            if (entity != null)
                entity.IsDeleted = false;
            repository.Update(entity);
        }
    }
}
