using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServices.Interfaces;
using DataServices.Providers;
using MainStaticMaintainableEntities.ModuleAssembly;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EDC2Reporting.WebAPI.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class ModuleApiController : ControllerBase
    {
        private ILogger<ModuleApiController> logger;
        IRepository<Module> repository;
        RepositoryOptions repositorySettings;

        public ModuleApiController(ILogger<ModuleApiController> _logger, 
            IOptionsSnapshot<RepositoryOptions> options)
        {
            logger = _logger;
            repositorySettings = options.Value;
            var locator = new RepositoryLocator<Module>();
            repository = locator.GetRepository(repositorySettings.RepositoryType);
        }

        // GET: api/<ModuleApiController>
        [HttpGet]
        public IEnumerable<Module> Get()
        {
            return repository.GetAll().Where(x => x.IsDeleted == false).ToList();
        }

        // GET api/<ModuleApiController>/5
        [HttpGet("{id}")]
        public Module Get(int id)
        {
            return repository.GetById(id);
        }

        // POST api/<ModuleApiController>
        [HttpPost]
        public void Post([FromBody] Module module)
        {
            repository.UpsertActivation(module);
        }

        // PUT api/<ModuleApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Module m)
        {
            repository.Update(m);
        }

        // DELETE api/<ModuleApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var entity = repository.GetById(id);
            if (entity != null)
            repository.Delete(entity);
        }
    }
}
