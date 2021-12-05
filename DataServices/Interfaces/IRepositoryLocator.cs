using DataServices.Providers;
using Microsoft.EntityFrameworkCore;

namespace DataServices.Interfaces
{
    public interface IRepositoryLocator<T> where T : IPersistentEntity
    {
        IRepository<T> GetRepository(RepositoryType repositoryType, DbContext dbContext);
    }
}