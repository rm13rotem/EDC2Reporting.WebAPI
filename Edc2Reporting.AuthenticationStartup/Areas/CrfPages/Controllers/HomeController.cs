using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edc2Reporting.AuthenticationStartup.Areas.CrfPages.Models;
using Microsoft.AspNetCore.Mvc;

namespace Edc2Reporting.AuthenticationStartup.Areas.CrfPages.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactModel model)
        {
            ViewBag.Title = "Contact Us";

            throw new InvalidOperationException("Bad things happen to good people");

            return View(model);
        }
    }
}
