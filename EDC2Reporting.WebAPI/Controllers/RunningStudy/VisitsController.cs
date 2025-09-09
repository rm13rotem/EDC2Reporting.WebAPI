using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models.VisitAssembley;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SessionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Controllers.RunningStudy
{
    public class VisitsController : Controller
    {

        private readonly ISessionWrapper _session;
        private readonly ILogger<VisitsController> _logger;
        private readonly EdcDbContext _context;

        public VisitsController(ILogger<VisitsController> logger
            , EdcDbContext context
            , ISessionWrapper session)
        {
            _logger = logger;
            _session = session;
            _context = context;
        }

        // GET: Visits
        public IActionResult Index()
        {
            var visits = _context.Visits.ToList();
            return View(visits);
        }

        // GET: Visits/Details/5
        public async Task<IActionResult> DetailsAsync(int? id)
        {
            if (id == null) return NotFound();

            var visit = await _context.Visits.FindAsync(id);
            if (visit == null) return NotFound();

            return View(visit);
        }

        // GET: Visits/Create
        public IActionResult Create()
        {
            ViewBag.CrfPages = _context.CrfPages.ToList();
            return View();
        }

        // POST: Visits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,JsonValue")] Visit visit)
        {
            if (ModelState.IsValid)
            {
                visit.GuidId = Guid.NewGuid().ToString();
                // TODO : visit.CreatedDt
                _context.Add(visit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CrfPages = _context.CrfPages.ToList();
            return View(visit);
        }

        // GET: Visits/Edit/5
        public IActionResult Edit(int id)
        {
            var visit = _context.Visits.Find(id);
            if (visit == null) return NotFound();

            ViewBag.CrfPages = _context.CrfPages.ToList();
            return View(visit);
        }

        // POST: Visits/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,JsonValue,GuidId")] Visit visit)
        {
            if (id != visit.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(visit.GuidId))
                        visit.GuidId = Guid.NewGuid().ToString();
                    _context.Update(visit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Visits.Any(e => e.Id == visit.Id))
                        return NotFound();
                    else throw;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Edit (POST) failed in Visits Controller.");

                    ViewBag.CrfPages = _context.CrfPages.ToList();
                    return View(visit);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CrfPages = _context.CrfPages.ToList();
            return View(visit);
        }

        // GET: Visits/Delete/5
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            if (id == null) return NotFound();

            var visit = await _context.Visits.FindAsync(id);
            if (visit == null) return NotFound();

            return View(visit);
        }

        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAsync(int id)
        {
            var visit = await _context.Visits.FindAsync(id);
            visit.IsDeleted = true;
            _context.Visits.Update(visit);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UndeleteAsync(int id)
        {
            var visit = await _context.Visits.FindAsync(id);
            visit.IsDeleted = false;
            _context.Visits.Update(visit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ----------------- EXTRA ACTIONS -----------------

        // Select Visit and redirect to first CRF
        public async Task<IActionResult> SelectVisit(int id)
        {
            var visit = await  _context.Visits.FindAsync(id);
            if (visit == null) return NotFound();

            // Store in session
            _session.SelectedVisitId = visit.Id;

            var crfIds = JsonConvert.DeserializeObject<List<int>>(visit.JsonValue);
            if (crfIds == null || !crfIds.Any()) return RedirectToAction(nameof(Index));

            if (_session.SelectedCrfId == 0) {
                var firstCrf = _context.CrfPages.FirstOrDefault(c => c.Id == crfIds.First());
                if (firstCrf == null) return RedirectToAction(nameof(Index));
                _session.SelectedCrfId = firstCrf.Id;
            }
            

                return RedirectToAction("Create", "CrfEntries", new { crfPageId = _session.SelectedCrfId, PatientId = _session.SelectedPatientId });
        }

        // Move to next CRF page in current Visit
        public async Task<IActionResult> NextCrfPageAsync()
        {
            int currentCrfId = _session.SelectedCrfId;;
            var visitId = _session.SelectedVisitId;
            if (visitId == 0) return RedirectToAction(nameof(Index));

            var visit = await _context.Visits.FindAsync(visitId);
            var crfIds = JsonConvert.DeserializeObject<List<int>>(visit.JsonValue);
            var idx = crfIds.IndexOf(currentCrfId);
            if (idx == -1 || idx + 1 >= crfIds.Count)
                return RedirectToAction(nameof(Index));

            _session.SelectedCrfId = crfIds[idx + 1];

            return RedirectToAction("Create", "CrfEntries", new { crfPageId = _session.SelectedCrfId, PatientId = _session.SelectedPatientId });

        }
    }
}
