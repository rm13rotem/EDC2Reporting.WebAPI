using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EDC2Reporting.WebAPI.Controllers
{
    public class PersistentEntityController : Controller
    {
        private readonly EdcDbContext db;

        public PersistentEntityController(EdcDbContext _db)
        {
            db = _db;
        }
       

        // GET: PersistentEntityController
        public ActionResult Index()
        {
            var list = db.PersistentEntity.ToList();
            return View(list);
        }

        // GET: PersistentEntityController/Details/5
        public ActionResult Details(int id)
        {
            var item = db.PersistentEntity.Find(id);
            return View();
        }

        // GET: PersistentEntityController/Create
        public ActionResult Create()
        {
            var item = new PersistentEntity();
            return View();
        }

        // POST: PersistentEntityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersistentEntity model)
        {
            try
            {
                if (true)
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
