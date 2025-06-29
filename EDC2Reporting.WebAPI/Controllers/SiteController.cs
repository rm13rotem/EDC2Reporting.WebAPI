using DataServices.Interfaces;
using DataServices.Providers;
using DataServices.SqlServerRepository.Models;
using MainStaticMaintainableEntities.SiteAssembly;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;

namespace EDC2Reporting.WebAPI.Controllers
{
    public class SiteController : Controller
    {
        private readonly ILogger<SiteController> _logger;
        private IRepository<Site> repository;
        private IRepository<Country> countryRepository;
        private IRepository<City> cityRepository;
        private readonly RepositoryOptions repoOptions;

        public SiteController(ILogger<SiteController> logger, IOptionsSnapshot<RepositoryOptions> options)
        {
            _logger = logger;
            repoOptions = options.Value;
            RepositoryLocator<Country> country_repositoryLocator = new RepositoryLocator<Country>();
            countryRepository = country_repositoryLocator.GetRepository(repoOptions.RepositoryType);
            RepositoryLocator<City> city_repositoryLocator = new RepositoryLocator<City>();
            cityRepository = city_repositoryLocator.GetRepository(repoOptions.RepositoryType);
            RepositoryLocator<Site> repositoryLocator = new RepositoryLocator<Site>();
            repository = repositoryLocator.GetRepository(repoOptions.RepositoryType);
        }

        public IViewComponentResult SelectSitePartialView(int? SiteId = 0)
        {
            var Site = new Site();
            if (SiteId > 0)
                Site = repository.GetById((int)SiteId);

            if (Site != null)
            {
                ViewBag["CountryId"] = Country.GetCountrySelectList(countryRepository,Site.CountryId);
                ViewBag["CityId"] = City.GetCitySelectList(cityRepository, Site.CountryId, Site.CityId);
                ViewBag["SiteId"] = Site.GetCitySelectList(repository, Site.CountryId, Site.CityId, Site.Id);
            }
            else
            {
                ViewBag["CountryId"] = Country.GetCountrySelectList(countryRepository, 0);
                ViewBag["CityId"] = City.GetCitySelectList(cityRepository, 0,0);
                ViewBag["SiteId"] = Site.GetCitySelectList(repository, 0,0,0);
            }

            return View(Site) as IViewComponentResult;
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
                repository.UpsertActivation(site);
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
            ViewBag.Countries = ExtractSelectList(countryRepository.GetAll(), model.CountryId);
            ViewBag.Cities = ExtractSelectList(cityRepository.GetAll(), model.CityId);
            ViewBag.Sites = ExtractSelectList(repository.GetAll(), model.Id);

            return View(model);
        }


        // POST: SiteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Site site)
        {
            try
            {
                SiteFactory siteFactory = new SiteFactory(repoOptions);
                repository.Update(site);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Countries = ExtractSelectList(countryRepository.GetAll(), site.CountryId);
                ViewBag.Cities = ExtractSelectList(cityRepository.GetAll(), site.CityId);
                ViewBag.Sites = ExtractSelectList(repository.GetAll(), site.Id);

                return View(site);
            }
        }

        // GET: SiteController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = repository.GetById(id);
            repository.Delete(model);
            return RedirectToAction(nameof(Index));
        }


        private List<SelectListItem> ExtractSelectList(IEnumerable<IPersistentEntity> list, int selectValue)
        {
            var entities = list.OrderBy(x => x.Id).ToList();
            entities.Insert(0, new PersistentEntity() { Id = 0, Name = "--UnSelected--" });
            return entities
                .Select(x => new SelectListItem(x.Name, x.Id.ToString(), x.Id == selectValue)).ToList();
        }
    }
}
