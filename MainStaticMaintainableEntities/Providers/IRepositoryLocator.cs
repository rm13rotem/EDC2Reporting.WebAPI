using MainStaticMaintainableEntities.Providers;
using Microsoft.EntityFrameworkCore;

namespace MainStaticMaintainableEntities
{
    public interface IRepositoryLocator<T> where T : PersistantEntity
    {
        IRepository<T> GetRepository(RepositoryType repositoryType, DbContext dbContext);
    }
}