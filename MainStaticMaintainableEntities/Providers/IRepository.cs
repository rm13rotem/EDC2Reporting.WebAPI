using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MainStaticMaintainableEntities.Providers
{
    public interface IRepository<T> where T : PersistantEntity
    {
        T Get(int Id, Guid GuidId);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByExpression(Expression<Func<T, bool>> expression);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
