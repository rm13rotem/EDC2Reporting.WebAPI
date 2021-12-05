using DataServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataServices.Providers
{
    public class RepositoryLocator<T> : IRepositoryLocator<T> where T : IPersistentEntity
    {
        public IRepository<T> GetRepository(RepositoryType repositoryType, DbContext dbContext)
        {
            if (repositoryType == RepositoryType.FromJsonRepository)
                return new FromJsonRepository<T>(typeof(T).Name);
            //if (repositoryType == RepositoryType.FromDbRepository)
            //    return new FromDbRepository<T>(dbContext);
            return null;

        }
    }
}
