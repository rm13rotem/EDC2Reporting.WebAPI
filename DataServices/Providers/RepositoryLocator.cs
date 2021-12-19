using DataServices.Interfaces;
using DataServices.SqlServerRepository;
using Microsoft.EntityFrameworkCore;

namespace DataServices.Providers
{
    public class RepositoryLocator<T> : IRepositoryLocator<T> where T : IPersistentEntity, new()
    {
        RepositoryType cachedRepositoryType;
        IRepository<T> cachedRepository;

        public IRepository<T> GetRepository(RepositoryType repositoryType, bool isForceRefresh = false)
        {
            if (cachedRepositoryType == repositoryType && cachedRepository != null && !isForceRefresh)
                return cachedRepository;

            if (repositoryType == RepositoryType.FromJsonRepository)
            {
                cachedRepository = new FromJsonRepository<T>(typeof(T).Name);
                cachedRepository = new BaseInMemoryRepository<T>(cachedRepository);
            }

            if (repositoryType == RepositoryType.FromDbRepository)
            {
                DbRepositoryLocator<T> repositoryLocator = new DbRepositoryLocator<T>();
                cachedRepository = repositoryLocator.GetRepository();
                cachedRepository = new BaseInMemoryRepository<T>(cachedRepository);
            }

            return cachedRepository;

        }
    }
}
