using DataServices.Providers;
using DataServices.SqlServerRepository;
using Microsoft.EntityFrameworkCore;

namespace DataServices.Interfaces
{
    public interface IRepositoryLocator<T> where T : IPersistentEntity
    {
        IRepository<T> GetRepository(RepositoryType repositoryType, EdcDbContext dbContext, bool isForcedReload);
    }
}