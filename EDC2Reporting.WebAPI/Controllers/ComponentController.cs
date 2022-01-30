using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Components;
using EDC2Reporting.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EDC2Reporting.WebAPI.Controllers
{
    public class ComponentController : Controller
    {
        private readonly ThreatsModelSingleton usageLog;

        public ComponentController()
        {
            usageLog = ThreatsModelSingleton.GetInstance();
            usageLog.ControllerUsage.LogUsage(nameof(ComponentController));
        }

        [ResponseCache(Duration = 300, VaryByQueryKeys = new string[] { "*" },
            Location = ResponseCacheLocation.Any)]
        public IActionResult BoolPartialView(ComponentFilter boolComponent)
        {
            usageLog.ControllerUsage.LogUsage(nameof(ComponentController), nameof(BoolPartialView));
            return View(boolComponent);
        }

        [ResponseCache(Duration = 300, VaryByQueryKeys = new string[] { "*" },
            Location = ResponseCacheLocation.Any)]
        public IActionResult DatePartialView(DateComponent component)
        {
            usageLog.ControllerUsage.LogUsage(nameof(ComponentController), nameof(DatePartialView));
            return View(component);
        }

        [ResponseCache(Duration = 300, VaryByQueryKeys = new string[] { "*" },
            Location = ResponseCacheLocation.Any)]
        public IActionResult DateTimePartialView(DateTimeComponent component)
        {
            usageLog.ControllerUsage.LogUsage(nameof(ComponentController), nameof(DateTimePartialView));

            return View(component);
        }


        [ResponseCache(Duration = 300, VaryByQueryKeys = new string[] { "*" },
            Location = ResponseCacheLocation.Any)]
        public IActionResult RangePartialView(RangeComponent component)
        {
            usageLog.ControllerUsage.LogUsage(nameof(ComponentController), nameof(RangePartialView));

            return View(component);
        }

        [ResponseCache(Duration = 300, VaryByQueryKeys = new string[] { "*" },
            Location = ResponseCacheLocation.Any)]
        public IActionResult SelectPartialView(SelectComponent selectComponent)
        {
            usageLog.ControllerUsage.LogUsage(nameof(ComponentController), nameof(SelectPartialView));

            return View(selectComponent);
        }

        [ResponseCache(Duration = 300, VaryByQueryKeys = new string[] { "*" }, 
            Location = ResponseCacheLocation.Any)]
        public IActionResult TextPartialView(TextComponent textComponent)
        {
            usageLog.ControllerUsage.LogUsage(nameof(ComponentController), nameof(TextPartialView));

            return View(textComponent);
        }
    }
}
