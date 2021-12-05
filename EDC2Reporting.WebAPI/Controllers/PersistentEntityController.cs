using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MainStaticMaintainableEntities;

namespace EDC2Reporting.WebAPI.Controllers
{
    public class PersistentEntityController : Controller
    {
        private readonly ClinicalDataContext db = new ClinicalDataContext();

        // GET: PersistentEntityController
        public ActionResult Index()
        {
            var list = db.PersistentEntity.ToList();
            return View(list);
        }

        // GET: PersistentEntityController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PersistentEntityController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersistentEntityController/Create
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

        // GET: PersistentEntityController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PersistentEntityController/Edit/5
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

        // GET: PersistentEntityController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersistentEntityController/Delete/5
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
