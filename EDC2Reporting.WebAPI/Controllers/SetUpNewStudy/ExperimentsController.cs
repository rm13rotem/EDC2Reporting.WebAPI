using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models;
using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Controllers
{
    [Authorize(Roles = "Admin, ClinicalTrialLeader, InternationalReviewBoard")]
    public class ExperimentsController : Controller
    {
        private readonly EdcDbContext _context;

        public ExperimentsController(EdcDbContext context)
        {
            _context = context;
        }

        // GET: Experiments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Experiments.ToListAsync());
        }

        // GET: Experiments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiment = await _context.Experiments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (experiment == null)
            {
                return NotFound();
            }

            return View(experiment);
        }

        // GET: Experiments/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            Experiment experiment = new();
            experiment.UniqueIdentifier = Guid.NewGuid().ToString();
            return View(experiment);
        }

        // POST: Experiments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,UniqueIdentifier,HelsinkiApprovalNumber,CompanyName,CompanyId,Name")] Experiment experiment)
        {
            if (ModelState.IsValid)
            {
                HtmlSanitizer sanitizer = new HtmlSanitizer();
                experiment.HelsinkiApprovalNumber = sanitizer.Sanitize(experiment.HelsinkiApprovalNumber);
                experiment.Name = sanitizer.Sanitize(experiment.Name);
                // TODO - continue. or find somebody else to sanitize;

                _context.Add(experiment);
                await _context.SaveChangesAsync();
                if (experiment.Id == 0)
                    throw new Exception("Error! Experiment created with Id = 0");
                else
                {
                    // Create 100 new dummy patients for this new experiment.
                    for (int i = 0; i < 100; i++)
                    {
                        Patient p = QueueFactory<Patient>.GetNew();
                        p.Name = "UKN";
                        p.IsDeleted = false;
                        p.StudyId = experiment.Id;
                        p.GuidId = Guid.NewGuid().ToString();
                        p.SubjectIdInTrial = i + 1;

                        _context.Add(p);
                        await _context.SaveChangesAsync();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            // current user.ExperimentId = experiment.Id;  TODO - set current user's ExperimentId
            return View(experiment);
        }

        // GET: Experiments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiment = await _context.Experiments.FindAsync(id);
            if (experiment == null)
            {
                return NotFound();
            }

            HtmlSanitizer sanitizer = new HtmlSanitizer();
            experiment.HelsinkiApprovalNumber = sanitizer.Sanitize(experiment.HelsinkiApprovalNumber);
            experiment.Name = sanitizer.Sanitize(experiment.Name);
            return View(experiment);
        }

        // POST: Experiments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UniqueIdentifier,HelsinkiApprovalNumber,CompanyName,CompanyId,Name")] Experiment experiment)
        {
            if (id != experiment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(experiment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperimentExists(experiment.Id))
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
            return View(experiment);
        }

        // GET: Experiments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiment = await _context.Experiments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (experiment == null)
            {
                return NotFound();
            }

            return View(experiment);
        }

        // POST: Experiments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var experiment = await _context.Experiments.FindAsync(id);
            _context.Experiments.Remove(experiment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExperimentExists(int id)
        {
            return _context.Experiments.Any(e => e.Id == id);
        }
    }
}
