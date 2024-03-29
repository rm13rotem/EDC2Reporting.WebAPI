﻿using DataServices.Interfaces;
using DataServices.SqlServerRepository;
using System;

namespace DataServices.Providers
{
    public class DbRepositoryLocator<T> 
        where T : IPersistentEntity, new()
    {    

        public IRepository<T> GetRepository()
        {
            EdcDbContext dbContext = new EdcDbContext();
            return  new FromDbRepository<T>(dbContext);
        }
    }
}