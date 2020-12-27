using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MainStaticMaintainableEntities.Providers
{
    public abstract class FromDbRepository<T> : IRepository<T> 
        where T : PersistantEntity
    {
        private readonly DbContext _dataContext;
        // Todo : Add Transaction by
        // https://gunnarpeipman.com/ef-core-repository-unit-of-work/

        public FromDbRepository(DbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _dataContext.Entry<T>(entity).State = EntityState.Modified;
            _dataContext.SaveChanges();
        }

        public T Get(int Id, Guid id)
        {
            return _dataContext.Set<T>().FirstOrDefault(x=>x.Id == Id && x.GuidId == id.ToString());
        }

        public void Insert(T entity)
        {
            _dataContext.Set<T>().Add(entity);
            _dataContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _dataContext.Set<T>().ToList();
        }

        public IEnumerable<T> GetByExpression(Expression<Func<T, bool>> expression)
        {
            return _dataContext.Set<T>().Where(expression).ToList();
        }

        public void Update(T entity)
        {
            _dataContext.Entry<T>(entity).State = EntityState.Modified;
            _dataContext.SaveChanges();
        }
    }
}
