using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models.CrfModels;
using EDC2Reporting.WebAPI.Filters;
using EDC2Reporting.WebAPI.Models;
using EDC2Reporting.WebAPI.Models.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Controllers
{
    public class CrfPagesController : Controller
    {
        private readonly ICrfPageManager _manager;
        private readonly EdcDbContext _db;

        public CrfPagesController(EdcDbContext db)
        {
            _manager = new CrfPageManager(db);
            _db = db;
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

        // NEW: GET: CrfPages/GetFromExternal/5
        public async Task<IActionResult> GetFromExternal(int id)
        {
            if (id <= 0) return BadRequest("Invalid remote id.");

            var importer = new CrfImporter(_db);
            var baseUrl = "https://www.crfdesign.somee.com/RenderCrfComponent/Index";

            string html;
            try
            {
                html = await importer.GetHtmlFromRemote(baseUrl, id);
            }
            catch (Exception ex)
            {
                // In production consider logging the exception with ILogger
                return BadRequest($"Failed to fetch remote HTML: {ex.Message}");
            }

            var newPage = importer.ParseNewCrfPage(html);
            if (newPage == null) return BadRequest("Failed to parse remote CRF HTML.");

            await importer.SaveAsyncToDb(newPage);

            return RedirectToAction(nameof(Index));
        }

    }
}
