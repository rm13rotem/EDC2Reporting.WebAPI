using DataServices.Interfaces;
using DataServices.Providers;
using DataServices.SqlServerRepository;
using MainStaticMaintainableEntities.SiteAssembly;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace EDC2Reporting.WebAPI.Controllers
{
    public class SiteController : Controller
    {
        private readonly ILogger<SiteController> _logger;
        private IRepository<Site> repository;
        private readonly RepositoryOptions repoOptions;

        public SiteController(ILogger<SiteController> logger, IOptionsSnapshot<RepositoryOptions> options)
        {
            _logger = logger;
            repoOptions = options.Value;
            RepositoryLocator<Site> repositoryLocator = new RepositoryLocator<Site>();
            repository = repositoryLocator.GetRepository(repoOptions.RepositoryType);
        }

        // GET: SiteController
        public ActionResult Index()
        {
            List<Site> list = repository.GetAll().ToList();
            return View(list);
        }

        // GET: SiteController/Details/5
        public ActionResult Details(int id)
        {
            var model = repository.GetById(id);
            return View(model);
        }

        // GET: SiteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SiteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Site site)
        {
            try
            {
                repository.InsertUpdateOrUndelete(site);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(site);
            }
        }

        // GET: SiteController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = repository.GetById(id);
            return View(model);
        }

        // POST: SiteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Site site)
        {
            try
            {
                repository.Update(site);
                return RedirectToAction(nameof(Index));
            }
            catch
            {

                return View(site);
            }
        }

        // GET: SiteController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = repository.GetById(id);
            repository.Delete(model);
            return View("Index");
        }
        
    }
}
