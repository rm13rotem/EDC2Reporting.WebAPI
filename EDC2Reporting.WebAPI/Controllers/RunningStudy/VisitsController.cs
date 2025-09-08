using DataServices.Interfaces;
using DataServices.Providers;
using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models.CrfModels;
using MainStaticMaintainableEntities.VisitAssembly;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

        private readonly SessionWrapper _session;
        private readonly ILogger<VisitsController> _logger;
        private IRepository<Visit> repository;
        private readonly RepositoryOptions repoOptions;
        private readonly EdcDbContext _context;

        public VisitsController(ILogger<VisitsController> logger
            , EdcDbContext context
            , IOptionsSnapshot<RepositoryOptions> options
            , SessionWrapper session)
        {
            _logger = logger;
            repoOptions = options.Value;
            RepositoryLocator<Visit> country_repositoryLocator = RepositoryLocator<Visit>.Instance;
            repository = country_repositoryLocator.GetRepository(repoOptions.RepositoryType);
            _session = session;
            _context = context;
        }

        // GET: Visits
        public IActionResult Index()
        {
            var visits = repository.GetAll();
            return View(visits);
        }

        // GET: Visits/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var visit = repository.GetById((int)id);
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
            var visit = repository.GetById(id);
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
                    repository.Update(visit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!repository.GetAll().Any(e => e.Id == visit.Id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CrfPages = _context.CrfPages.ToList();
            return View(visit);
        }

        // GET: Visits/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var visit = repository.GetById((int)id);
            if (visit == null) return NotFound();

            return View(visit);
        }

        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var visit = repository.GetById(id);
            visit.IsDeleted = true;
            repository.Update(visit);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Undelete(int id)
        {
            var visit = repository.GetById(id);
            visit.IsDeleted = false;
            repository.Update(visit);
            return RedirectToAction(nameof(Index));
        }

        // ----------------- EXTRA ACTIONS -----------------

        // Select Visit and redirect to first CRF
        public IActionResult SelectVisit(int id)
        {
            var visit = repository.GetById(id);
            if (visit == null) return NotFound();

            // Store in session
            _session.SelectedVisitId = visit.Id;

            var crfIds = JsonConvert.DeserializeObject<List<int>>(visit.JsonValue);
            if (crfIds == null || !crfIds.Any()) return RedirectToAction(nameof(Index));

            var firstCrf = _context.CrfPages.FirstOrDefault(c => c.Id == crfIds.First());
            if (firstCrf == null) return RedirectToAction(nameof(Index));
            _session.SelectedCrfId = firstCrf.Id;

            return RedirectToAction("Create", "CrfEntries", new { crfPageId = firstCrf.Id, PatientId = 0 });
        }

        // Move to next CRF page in current Visit
        public IActionResult NextCrfPage(int currentCrfId)
        {
            var visitId = _session.SelectedVisitId;
            if (visitId == 0) return RedirectToAction(nameof(Index));

            var visit = repository.GetById(visitId);
            var crfIds = JsonConvert.DeserializeObject<List<int>>(visit.JsonValue);
            var idx = crfIds.IndexOf(currentCrfId);
            if (idx == -1 || idx + 1 >= crfIds.Count)
                return RedirectToAction(nameof(Index));

            int nextCrfId = crfIds[idx + 1];
            return RedirectToAction("Details", "CrfEntries", new { id = nextCrfId });
        }
    }
}
