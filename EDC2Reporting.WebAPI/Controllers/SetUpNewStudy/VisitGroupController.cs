    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using DataServices.SqlServerRepository;
    using DataServices.SqlServerRepository.Models.VisitGroup;
    using DataServices.SqlServerRepository.Models.VisitAssembley;

namespace EDC2Reporting.WebAPI.Controllers.SetUpNewStudy
{

        public class VisitGroupController : Controller
        {
            private readonly EdcDbContext _context;

            public VisitGroupController(EdcDbContext context)
            {
                _context = context;
            }

            // GET: VisitGroups
            public async Task<IActionResult> Index()
            {
                var groups = await _context.VisitGroups
                    .Include(vg => vg.Visits)
                    .ToListAsync();

                return View(groups);
            }

            // GET: VisitGroups/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null) return NotFound();

                var visitGroup = await _context.VisitGroups
                    .Include(vg => vg.Visits)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (visitGroup == null) return NotFound();

                return View(visitGroup);
            }

            // GET: VisitGroups/Create
            public IActionResult Create()
            {
                ViewData["VisitIds"] = new MultiSelectList(_context.Visits, "Id", "Name");
                return View();
            }

            // POST: VisitGroups/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(VisitGroup visitGroup, int[] selectedVisits)
            {
                if (ModelState.IsValid)
                {
                    if (selectedVisits != null)
                    {
                        visitGroup.Visits = await _context.Visits
                            .Where(v => selectedVisits.Contains(v.Id))
                            .ToListAsync();
                    }

                    _context.Add(visitGroup);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                ViewData["VisitIds"] = new MultiSelectList(_context.Visits, "Id", "Name", selectedVisits);
                return View(visitGroup);
            }

            // GET: VisitGroups/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null) return NotFound();

                var visitGroup = await _context.VisitGroups
                    .Include(vg => vg.Visits)
                    .FirstOrDefaultAsync(vg => vg.Id == id);

                if (visitGroup == null) return NotFound();

                ViewData["VisitIds"] = new MultiSelectList(
                    _context.Visits, "Id", "Name", visitGroup.Visits.Select(v => v.Id));

                return View(visitGroup);
            }

        // POST: VisitGroups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] VisitGroup visitGroup, int[] selectedVisits)
        {
            if (id != visitGroup.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingGroup = await _context.VisitGroups
                        .Include(vg => vg.Visits)
                        .FirstOrDefaultAsync(vg => vg.Id == id);

                    if (existingGroup == null) return NotFound();

                    existingGroup.Name = visitGroup.Name;

                    // update visits
                    existingGroup.Visits.Clear();
                    var visits = await _context.Visits.Where(v => selectedVisits.Contains(v.Id)).ToListAsync();
                    foreach (var v in visits)
                        existingGroup.Visits.Add(v);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.VisitGroups.Any(e => e.Id == visitGroup.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Visits"] = new MultiSelectList(_context.Visits, "Id", "Name", selectedVisits);
            return View(visitGroup);
        }

        // GET: VisitGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var visitGroup = await _context.VisitGroups
                .Include(vg => vg.Visits)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (visitGroup == null) return NotFound();

            return View(visitGroup);
        }

        // POST: VisitGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visitGroup = await _context.VisitGroups.FindAsync(id);
            if (visitGroup != null)
            {
                _context.VisitGroups.Remove(visitGroup);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}