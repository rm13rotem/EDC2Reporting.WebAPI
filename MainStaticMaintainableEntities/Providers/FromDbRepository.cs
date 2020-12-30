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
        private readonly ClinicalDataContext _dataContext;
        // Todo : Add Transaction by
        // https://gunnarpeipman.com/ef-core-repository-unit-of-work/

        public FromDbRepository(ClinicalDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _dataContext.Entry<T>(entity).State = EntityState.Modified;
            _dataContext.SaveChanges();
        }

        public T GetById(int Id)
        {
            return _dataContext.Set<T>().FirstOrDefault(x=>x.Id == Id);
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
