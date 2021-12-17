using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EDC2Reporting.WebAPI.Controllers
{
    public class ModuleInfoController : Controller
    {
        private readonly EdcDbContext _context;

        public ModuleInfoController(EdcDbContext context)
        {
            _context = context;
        }

        // GET: ModuleInfo
        public async Task<IActionResult> Index()
        {
            return View(await _context.ModuleInfos.ToListAsync());
        }

        // GET: ModuleInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moduleInfo = await _context.ModuleInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moduleInfo == null)
            {
                return NotFound();
            }

            return View(moduleInfo);
        }

        // GET: ModuleInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModuleInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExperimentId,VisitGroupId,VisitId,DoctorId,PatientId,ModuleId,DataInJson,CurrentDataInJson,LastUpdator,CreatedDateTime,LastUpdatedDateTime,CurrentLastUpdateDateTime")] ModuleInfo moduleInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moduleInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moduleInfo);
        }

        // GET: ModuleInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moduleInfo = await _context.ModuleInfos.FindAsync(id);
            if (moduleInfo == null)
            {
                return NotFound();
            }
            return View(moduleInfo);
        }

        // POST: ModuleInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExperimentId,VisitGroupId,VisitId,DoctorId,PatientId,ModuleId,DataInJson,CurrentDataInJson,LastUpdator,CreatedDateTime,LastUpdatedDateTime,CurrentLastUpdateDateTime")] ModuleInfo moduleInfo)
        {
            if (id != moduleInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moduleInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleInfoExists(moduleInfo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(moduleInfo);
        }

        // GET: ModuleInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moduleInfo = await _context.ModuleInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moduleInfo == null)
            {
                return NotFound();
            }

            return View(moduleInfo);
        }

        // POST: ModuleInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moduleInfo = await _context.ModuleInfos.FindAsync(id);
            _context.ModuleInfos.Remove(moduleInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleInfoExists(int id)
        {
            return _context.ModuleInfos.Any(e => e.Id == id);
        }
    }
}
