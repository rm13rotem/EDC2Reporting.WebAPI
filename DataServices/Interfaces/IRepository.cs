using DataServices.SqlServerRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataServices.Interfaces
{
    public interface IRepository<T> where T : IPersistentEntity
    {
        T GetById(int Id);
        IEnumerable<T> GetAll(bool isForceReload = false);
        void InsertUpdateOrUndelete(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
