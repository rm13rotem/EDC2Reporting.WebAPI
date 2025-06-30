using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models;
using EDC2Reporting.WebAPI.Models.SiteModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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


        // GET: PersistentEntity/Load
        [HttpGet]
        public IActionResult Load()
        {
            // Load Many from json
            return View();
        }

        // POST: PersistentEntity/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Load(PersistentEntity persistentEntity)
        {
            List<CountryViewModel> countryList = null;
            //JSON here is a list of entities
            if (persistentEntity.EntityName.ToLower() == "country")
                countryList = JsonConvert.DeserializeObject<List<CountryViewModel>>(persistentEntity.JsonValue);

            if (!string.IsNullOrWhiteSpace(persistentEntity.JsonValue))
            {
                if (countryList != null && countryList.Any())
                {
                    for (int i = 0; i < countryList.Count; i++)
                    {
                        var exist = db.PersistentEntities.
                            FirstOrDefault(x => x.EntityName.ToLower() == "country" &&
                            x.Name.ToLower() == countryList[i].Name.ToLower());
                        if (exist == null)
                        {
                            countryList[i].Id = i;
                            var country = new PersistentEntity()
                            {
                                EntityName = "country",
                                Name = countryList[i].Name,
                                GuidId = Guid.NewGuid().ToString(),
                                CreateDate = DateTime.UtcNow,
                                IsDeleted = false,
                                JsonValue = JsonConvert.SerializeObject(countryList[i])
                            };
                            db.Add(country);
                            await db.SaveChangesAsync();
                        }
                    }
                }
                else
                {
                    persistentEntity.GuidId = Guid.NewGuid().ToString();
                    persistentEntity.CreateDate = DateTime.UtcNow;
                    persistentEntity.IsDeleted = false;
                                
                    db.Add(persistentEntity);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
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
