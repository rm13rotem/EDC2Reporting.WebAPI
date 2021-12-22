using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            var list = db.PersistentEntities.ToList();
            return View(list);
        }


        // GET: PersistentEntity/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persistentEntity = await db.PersistentEntities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persistentEntity == null)
            {
                return NotFound();
            }

            return View(persistentEntity);
        }

        // GET: PersistentEntity/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersistentEntity/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersistentEntity persistentEntity)
        {
            if (ModelState.IsValid)
            {
                db.Add(persistentEntity);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(persistentEntity);
        }

        // GET: PersistentEntity/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persistentEntity = await db.PersistentEntities.FindAsync(id);
            if (persistentEntity == null)
            {
                return NotFound();
            }
            return View(persistentEntity);
        }

        // POST: PersistentEntity/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GuidId,EntityName,CreateDate,Name,JsonValue,IsDeleted")] PersistentEntity persistentEntity)
        {
            if (id != persistentEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(persistentEntity);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersistentEntityExists(persistentEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(persistentEntity);
        }

        // GET: PersistentEntity/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persistentEntity = await db.PersistentEntities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persistentEntity == null)
            {
                return NotFound();
            }

            return View(persistentEntity);
        }

        // POST: PersistentEntity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persistentEntity = await db.PersistentEntities.FindAsync(id);
            db.PersistentEntities.Remove(persistentEntity);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersistentEntityExists(int id)
        {
            return db.PersistentEntities.Any(e => e.Id == id);
        }
    }
}
