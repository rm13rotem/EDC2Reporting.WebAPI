using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models.CrfModels;
using EDC2Reporting.WebAPI.Models.Managers;
using EDC2Reporting.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Controllers
{

    namespace EDC2Reporting.Web.Controllers
    {
        public class CrfEntriesController : Controller
        {
            private readonly EdcDbContext _context;

            public CrfEntriesController(EdcDbContext context)
            {
                _context = context;
            }

            // GET: CrfEntries
            public async Task<IActionResult> Index()
            {
                var entries = await _context.CrfEntries
                                      .Include(e => e.CrfPage)
                                      .OrderByDescending(e => e.CreatedAt).ToListAsync();
                return View(entries);
            }

            // GET: CrfEntries/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null) return NotFound();

                var entry = await _context.CrfEntries
                                          .Include(e => e.CrfPage)
                                          .FirstOrDefaultAsync(m => m.Id == id);

                if (entry == null) return NotFound();

                return View(entry);
            }

            // GET: CrfEntries/Create?crfPageId=5&patientId=101
            public async Task<IActionResult> Create(int crfPageId, int patientId)
            {
                var crfPage = await _context.CrfPages.FirstOrDefaultAsync(p => p.Id == crfPageId);
                if (crfPage == null) return NotFound();
                ViewBag.CrfPage = crfPage;

                var entry = new CrfEntry
                {
                    CrfPageId = crfPage.Id,
                    PatientId = patientId,
                    StudyId = crfPage.StudyId, // TODO: plug in current study from doctor context
                    VisitId = 0, // TODO : plug in selected visitId
                    VisitIndex = 0, // TODO : plug in selected visitIndex
                    DoctorId = 0 // TODO: plug in current logged-in doctor
                };

                return View(entry);
            }

            // POST: CrfEntries/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("CrfPageId,DoctorId,PatientId,VisitId,VisitIndex,StudyId,FormDataJson")] CrfEntry entry)
            {
                if (ModelState.IsValid)
                {
                    entry.CreatedAt = DateTime.UtcNow;
                    _context.Add(entry);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                // else 
                var crfPage = await _context.CrfPages.FirstOrDefaultAsync(p => p.Id == entry.CrfPageId);
                if (crfPage == null) return NotFound();

                // repopulate ViewBag with FormDataJson
                var manager = new FormRenderingService();
                crfPage.Html = manager.Render(crfPage, entry.FormDataJson);
                ViewBag.CrfPage = crfPage;

                return View(entry);
            }

            // GET: CrfEntries/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null) return NotFound();

                var entry = await _context.CrfEntries.Include(e => e.CrfPage).FirstOrDefaultAsync(e => e.Id == id);
                if (entry == null) return NotFound();

                var crfPage = await _context.CrfPages.FirstOrDefaultAsync(p => p.Id == entry.CrfPageId);
                if (crfPage == null) return NotFound();

                // repopulate ViewBag with FormDataJson
                var manager = new FormRenderingService();
                crfPage.Html = manager.Render(crfPage, entry.FormDataJson);
                ViewBag.CrfPage = crfPage;


                return View(entry);
            }

            // POST: CrfEntries/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,CrfPageId,DoctorId,PatientId,VisitId,VisitIndex,StudyId,FormDataJson,CreatedAt")] CrfEntry entry)
            {
                if (id != entry.Id) return NotFound();

                if (ModelState.IsValid)
                {
                    try
                    {
                        entry.UpdatedAt = DateTime.UtcNow;
                        _context.Attach(entry);
                        _context.Update(entry);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!_context.CrfEntries.Any(e => e.Id == entry.Id))
                            return NotFound();
                        else
                            throw;
                    }
                    return RedirectToAction(nameof(Index));
                }

                //else - faulty postback
                var crfPage = await _context.CrfPages.FirstOrDefaultAsync(p => p.Id == entry.CrfPageId);
                if (crfPage == null) return NotFound();

                // repopulate ViewBag with FormDataJson
                var manager = new FormRenderingService();
                crfPage.Html = manager.Render(crfPage, entry.FormDataJson);
                ViewBag.CrfPage = crfPage;

                return View(entry);
            }

            // GET: CrfEntries/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null) return NotFound();

                var entry = await _context.CrfEntries.Include(e => e.CrfPage)
                                                     .FirstOrDefaultAsync(m => m.Id == id);
                if (entry == null) return NotFound();

                return View(entry);
            }
            
            // GET: CrfEntries/Undelete/5
            public async Task<IActionResult> Undelete(int? id)
            {
                if (id == null) return NotFound();

                var entry = await _context.CrfEntries.Include(e => e.CrfPage)
                                                     .FirstOrDefaultAsync(m => m.Id == id);
                if (entry == null) return NotFound();
                if (entry != null)
                {
                    entry.IsDeleted = false;
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            // POST: CrfEntries/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var entry = await _context.CrfEntries.FindAsync(id);
                if (entry != null)
                {
                    entry.IsDeleted = true;
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
        }
    }

}
