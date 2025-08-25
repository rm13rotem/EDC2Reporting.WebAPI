using DataServices.SqlServerRepository.Models.CrfModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Models.Managers
{
    public interface ICrfPageManager
    {
        Task CreateAsync(CrfPage page);
        Task DeleteAsync(int id);
        Task<List<CrfPage>> GetAllAsync();
        Task<CrfPage> GetByIdAsync(int id);
        Task<bool> UpdateAsync(CrfPage page);
    }
}