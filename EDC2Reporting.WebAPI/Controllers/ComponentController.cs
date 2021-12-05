using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Components;
using Microsoft.AspNetCore.Mvc;

namespace EDC2Reporting.WebAPI.Controllers
{
    public class ComponentController : Controller
    {
        public IActionResult BoolPartialView(BoolComponent boolComponent)
        {
            return View(boolComponent);
        }
        public IActionResult DatePartialView(DateComponent component)
        {
            return View(component);
        }
        public IActionResult DateTimePartialView(DateTimeComponent component)
        {
            return View(component);
        }
        public IActionResult RangePartialView(RangeComponent component)
        {
            return View(component);
        }
        public IActionResult SelectPartialView(SelectComponent selectComponent)
        {
            return View(selectComponent);
        }
        public IActionResult TextPartialView(TextComponent textComponent)
        {
            return View(textComponent);
        }
    }
}
