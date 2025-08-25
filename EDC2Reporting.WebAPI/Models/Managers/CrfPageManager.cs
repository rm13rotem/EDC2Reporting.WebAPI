using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models.CrfModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Models.Managers
{
    public class CrfPageManager : ICrfPageManager
    {
        private readonly EdcDbContext _db;

        public CrfPageManager(EdcDbContext db)
        {
            _db = db;
        }

        public async Task<List<CrfPage>> GetAllAsync()
        {
            return await _db.CrfPages.ToListAsync();
        }

        public async Task<CrfPage?> GetByIdAsync(int id)
        {
            return await _db.CrfPages
                .Include(p => p.Entries) // eager-load related entries
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CreateAsync(CrfPage page)
        {
            _db.Add(page);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(CrfPage page)
        {
            if (!_db.CrfPages.Any(p => p.Id == page.Id)) return false;

            _db.Update(page);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var page = await _db.CrfPages.FindAsync(id);
            if (page != null)
            {
                _db.CrfPages.Remove(page);
                await _db.SaveChangesAsync();
            }
        }
    }
}
