using MainStaticMaintainableEntities.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainStaticMaintainableEntities
{
    public class RepositoryLocator<T> : IRepositoryLocator<T> where T : PersistantEntity
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
