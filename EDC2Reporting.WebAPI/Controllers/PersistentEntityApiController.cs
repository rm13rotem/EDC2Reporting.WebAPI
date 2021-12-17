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
    public class PersistentEntityApiController : ControllerBase
    {
        private readonly EdcDbContext _context;

        public PersistentEntityApiController(EdcDbContext context)
        {
            _context = context;
        }

        // GET: api/PersistentEntityApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersistentEntity>>> GetPersistentEntities()
        {
            return await _context.PersistentEntities.ToListAsync();
        }

        // GET: api/PersistentEntityApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersistentEntity>> GetPersistentEntity(int id)
        {
            var persistentEntity = await _context.PersistentEntities.FindAsync(id);

            if (persistentEntity == null)
            {
                return NotFound();
            }

            return persistentEntity;
        }

        // PUT: api/PersistentEntityApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersistentEntity(int id, PersistentEntity persistentEntity)
        {
            if (id != persistentEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(persistentEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersistentEntityExists(id))
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

        // POST: api/PersistentEntityApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PersistentEntity>> PostPersistentEntity(PersistentEntity persistentEntity)
        {
            _context.PersistentEntities.Add(persistentEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersistentEntity", new { id = persistentEntity.Id }, persistentEntity);
        }

        // DELETE: api/PersistentEntityApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersistentEntity>> DeletePersistentEntity(int id)
        {
            var persistentEntity = await _context.PersistentEntities.FindAsync(id);
            if (persistentEntity == null)
            {
                return NotFound();
            }

            _context.PersistentEntities.Remove(persistentEntity);
            await _context.SaveChangesAsync();

            return persistentEntity;
        }

        private bool PersistentEntityExists(int id)
        {
            return _context.PersistentEntities.Any(e => e.Id == id);
        }
    }
}
