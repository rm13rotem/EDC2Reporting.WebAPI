using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Components;
using DataServices.Interfaces;
using DataServices.Providers;
using EDC2Reporting.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace EDC2Reporting.WebAPI.Controllers
{
    //[Authorize(Roles ="Administrator")]
    public class ComponentManagementController : Controller
    {
        private readonly ThreatsModelSingleton usageLog;
        private IRepository<AbstractComponent> repository;

        public ComponentManagementController(IOptionsSnapshot<RepositoryOptions> options)
        {
            usageLog = ThreatsModelSingleton.GetInstance();
            usageLog.ControllerUsage.LogUsage(nameof(ComponentController));
            var locator = new RepositoryLocator<AbstractComponent>();
            var repositorySettings = options.Value;
            repository = locator.GetRepository(repositorySettings.RepositoryType);
        }

        public IActionResult Index(ComponentFilter filter)
        {
            var currentList = filter.GetCurrentList(repository.GetAll()).ToList();
            ViewBag.filter = filter;
            ViewBag.ComponentType = ComponentFilter.GetSelectList(filter.ComponentType);
            return View(currentList);
        }

        public IActionResult Edit(AbstractComponent component)
        {
            usageLog.ControllerUsage.LogUsage(nameof(ComponentManagementController), nameof(Edit));
            ViewBag.ComponentType = ComponentFilter.GetSelectList(component.ComponentType);
            return View(component);
        }

        public IActionResult Create(AbstractComponent component)
        {
            ViewBag.ComponentType = ComponentFilter.GetSelectList(component.ComponentType);
            usageLog.ControllerUsage.LogUsage(nameof(ComponentController), nameof(Create));
            return View("Edit",component);
        }

        public IActionResult Duplicate(AbstractComponent component)
        {
            usageLog.ControllerUsage.LogUsage(nameof(ComponentController), nameof(Duplicate));
            component.Id = repository.GetAll().Max(x => x.Id) + 1;
            component.GuidId = Guid.NewGuid().ToString();
            repository.UpsertActivation(component);
            var filter = new ComponentFilter()
            {
                ComponentType = component.ComponentType,
                PartOfLabel = component.Label
            };
            return RedirectToAction("Index", filter);
        }

        public IActionResult Delete(AbstractComponent component)
        {
            usageLog.ControllerUsage.LogUsage(nameof(ComponentController), nameof(Delete));
            component.IsDeleted = true;
            repository.Delete(component);
            var filter = new ComponentFilter()
            {
                ComponentType = component.ComponentType,
                PartOfLabel = component.Label
            };
            return RedirectToAction("Index", filter);
        }

        public IActionResult UnDelete(AbstractComponent component)
        {
            usageLog.ControllerUsage.LogUsage(nameof(ComponentController), nameof(Delete));
            component.IsDeleted = false;
            repository.UpsertActivation(component);
            var filter = new ComponentFilter()
            {
                ComponentType = component.ComponentType,
                PartOfLabel = component.Label
            };
            return RedirectToAction("Index", filter);
        }

    }
}
