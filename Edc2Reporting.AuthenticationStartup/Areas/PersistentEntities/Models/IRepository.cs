using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edc2Reporting.AuthenticationStartup.Areas.PersistentEntities.Models
{
    public interface IRepository<T>
        
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<bool> TryAddAsync(T entity);
        Task<bool> TryUpdateAsync(T entity);
        Task<bool> TryDeleteAsync(int id);
        Task<bool> TryUnDeleteAsync(int id);
    }
}

