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
    public class ModuleInfoApiController : ControllerBase
    {
        private readonly EdcDbContext _context;

        public ModuleInfoApiController(EdcDbContext context)
        {
            _context = context;
        }

        // GET: api/ModuleInfoApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleInfo>>> GetModuleInfos()
        {
            return await _context.ModuleInfos.ToListAsync();
        }

        // GET: api/ModuleInfoApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModuleInfo>> GetModuleInfo(int id)
        {
            var moduleInfo = await _context.ModuleInfos.FindAsync(id);

            if (moduleInfo == null)
            {
                return NotFound();
            }

            return moduleInfo;
        }

        // PUT: api/ModuleInfoApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModuleInfo(int id, ModuleInfo moduleInfo)
        {
            if (id != moduleInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(moduleInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleInfoExists(id))
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

        // POST: api/ModuleInfoApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ModuleInfo>> PostModuleInfo(ModuleInfo moduleInfo)
        {
            _context.ModuleInfos.Add(moduleInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModuleInfo", new { id = moduleInfo.Id }, moduleInfo);
        }

        // DELETE: api/ModuleInfoApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ModuleInfo>> DeleteModuleInfo(int id)
        {
            var moduleInfo = await _context.ModuleInfos.FindAsync(id);
            if (moduleInfo == null)
            {
                return NotFound();
            }

            _context.ModuleInfos.Remove(moduleInfo);
            await _context.SaveChangesAsync();

            return moduleInfo;
        }

        private bool ModuleInfoExists(int id)
        {
            return _context.ModuleInfos.Any(e => e.Id == id);
        }
    }
}
