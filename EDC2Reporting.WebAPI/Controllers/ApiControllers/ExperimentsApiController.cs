using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EDC2Reporting.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperimentsApiController : ControllerBase
    {
        private readonly EdcDbContext _context;

        public ExperimentsApiController(EdcDbContext context)
        {
            _context = context;
        }

        // GET: api/ExperimentsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Experiment>>> GetExperiments()
        {
            return await _context.Experiments.ToListAsync();
        }

        // GET: api/ExperimentsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Experiment>> GetExperiment(int id)
        {
            var experiment = await _context.Experiments.FindAsync(id);

            if (experiment == null)
            {
                return NotFound();
            }

            return experiment;
        }

        // PUT: api/ExperimentsApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExperiment(int id, Experiment experiment)
        {
            if (id != experiment.Id)
            {
                return BadRequest();
            }

            _context.Entry(experiment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExperimentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ExperimentsApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Experiment>> PostExperiment(Experiment experiment)
        {
            _context.Experiments.Add(experiment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExperiment", new { id = experiment.Id }, experiment);
        }

        // DELETE: api/ExperimentsApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Experiment>> DeleteExperiment(int id)
        {
            var experiment = await _context.Experiments.FindAsync(id);
            if (experiment == null)
            {
                return NotFound();
            }

            _context.Experiments.Remove(experiment);
            await _context.SaveChangesAsync();

            return experiment;
        }

        private bool ExperimentExists(int id)
        {
            return _context.Experiments.Any(e => e.Id == id);
        }
    }
}
