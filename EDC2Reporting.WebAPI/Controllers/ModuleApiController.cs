using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServices.Interfaces;
using MainStaticMaintainableEntities.ModuleAssembly;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EDC2Reporting.WebAPI.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class ModuleApiController : ControllerBase
    {
        private ILogger<ModuleApiController> logger;
        IRepository<Module> repository;

        public ModuleApiController(ILogger<ModuleApiController> _logger, IRepository<Module> repo)
        {
            logger = _logger;
            repository = repo;
        }

        // GET: api/<ModuleApiController>
        [HttpGet]
        public IEnumerable<Module> Get()
        {
            return repository.GetAll();
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
            repository.Insert(module);
        }

        // PUT api/<ModuleApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ModuleApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
