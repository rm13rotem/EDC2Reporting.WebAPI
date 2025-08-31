using DataServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainStaticMaintainableEntities.SiteAssembly;
using Microsoft.Extensions.Logging;
using DataServices.Providers;
using Microsoft.Extensions.Options;
using EDC2Reporting.WebAPI.Models.SiteModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataServices.SqlServerRepository.Models;

namespace EDC2Reporting.WebAPI.Controllers.SetUpNewStudy
{
    public class CityController : Controller
    {
        private readonly ILogger<SiteController> _logger;
        private IRepository<Country> countryRepository;
        private IRepository<City> cityRepository;
        private readonly RepositoryOptions repoOptions;

        public CityController(ILogger<SiteController> logger, IOptionsSnapshot<RepositoryOptions> options)
        {
            _logger = logger;
            repoOptions = options.Value;
            RepositoryLocator<Country> country_repositoryLocator = new RepositoryLocator<Country>();
            countryRepository = country_repositoryLocator.GetRepository(repoOptions.RepositoryType);
            RepositoryLocator<City> city_repositoryLocator = new RepositoryLocator<City>();
            cityRepository = city_repositoryLocator.GetRepository(repoOptions.RepositoryType);

            if (CityViewModel.CountryRepository == null)
                CityViewModel.CountryRepository = countryRepository;
        
        }

        public IActionResult Index()
        {
            var cities = cityRepository.GetAll()
                .Select(x => new CityViewModel(x)).ToList();

            return View(cities);
        }

        // GET: City/Duplicate/5
        public async Task<IActionResult> Duplicate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var city = cityRepository.GetById((int)id);
            if (city == null)
            {
                return NotFound();
            }

            var newCity = new City()
            {
                GuidId = Guid.NewGuid().ToString(),
                CountryId = city.CountryId,
                Name = city.Name,
                IsDeleted = false, 
            };
            cityRepository.UpsertActivation(newCity);

            return RedirectToAction("Index");
        }

        // GET: City/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var city = cityRepository.GetById((int)id);
            if (city == null)
            {
                return NotFound();
            }
            city.IsDeleted = true;
            cityRepository.UpsertActivation(city);

            return RedirectToAction("Index");
        }

        // GET: SiteController/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = ExtractSelectList(countryRepository.GetAll(), 0);
            return View();
        }

        // POST: SiteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(City city)
        {
            try
            {
                city.GuidId = Guid.NewGuid().ToString();
                cityRepository.UpsertActivation(city);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.CountryId = ExtractSelectList(countryRepository.GetAll(), city.CountryId);
                return View(city);
            }
        }

        // GET: SiteController/Edit/5
        public ActionResult Edit(int id)
        {
            var city = cityRepository.GetById(id);
            ViewBag.CountryId = ExtractSelectList(countryRepository.GetAll(), city.CountryId);
            
            return View(city);
        }


        // POST: SiteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, City city)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(city.GuidId))
                    city.GuidId = Guid.NewGuid().ToString();
                cityRepository.Update(city);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.CountryId = ExtractSelectList(countryRepository.GetAll(), city.CountryId);
                
                return View(city);
            }
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
