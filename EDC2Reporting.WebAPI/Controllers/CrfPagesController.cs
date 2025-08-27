using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataServices.SqlServerRepository.Models.CrfModels;
using DataServices.SqlServerRepository;
using EDC2Reporting.WebAPI.Models.Managers;
using EDC2Reporting.WebAPI.Filters;

namespace EDC2Reporting.WebAPI.Controllers
{
    public class CrfPagesController : Controller
    {
        private readonly ICrfPageManager _manager;

        public CrfPagesController(EdcDbContext db)
        {
            _manager = new CrfPageManager(db);
        }

        // GET: CrfPages
        public async Task<IActionResult> Index()
        {
            var pages = await _manager.GetAllAsync();
            return View(pages);
        }

        // GET: CrfPages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var page = await _manager.GetByIdAsync(id.Value);
            if (page == null) return NotFound();

            return View(page);
        }

        // GET: CrfPages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CrfPages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudyId,Name,Html,Description,IsLockedForChanges,GuidId,IsDeleted")] CrfPage page)
        {
            if (ModelState.IsValid)
            {
                await _manager.CreateAsync(page);
                return RedirectToAction(nameof(Index));
            }
            return View(page);
        }

        // GET: CrfPages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var page = await _manager.GetByIdAsync(id.Value);
            if (page == null) return NotFound();

            return View(page);
        }

        // POST: CrfPages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudyId,Name,Html,Description,IsLockedForChanges,GuidId,IsDeleted")] CrfPage page)
        {
            if (id != page.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var updated = await _manager.UpdateAsync(page);
                if (!updated) return NotFound();

                return RedirectToAction(nameof(Index));
            }
            return View(page);
        }

        // GET: CrfPages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var page = await _manager.GetByIdAsync(id.Value);
            if (page == null) return NotFound();

            return View(page);
        }

        // POST: CrfPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _manager.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
