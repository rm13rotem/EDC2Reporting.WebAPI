using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EDC2Reporting.WebAPI.Controllers
{
    public class SiteController : Controller
    {
        // GET: SiteController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SiteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SiteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SiteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SiteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SiteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SiteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SiteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
