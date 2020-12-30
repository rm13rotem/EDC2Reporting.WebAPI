using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Utilities;

namespace MainStaticMaintainableEntities.Providers
{ 
    public abstract class BaseInMemoryRepository<T> : IRepository<T> where T : PersistantEntity
    {
        private IRepository<T> repository;
        private readonly List<T> _myList;
        private readonly List<T> _myOriginalList;

        public BaseInMemoryRepository(IRepository<T> _repository)
        {
            repository = _repository;
            _myList = repository.GetAll().ToList();
            _myOriginalList = repository.GetAll().ToList();
        }

        public BaseInMemoryRepository(List<T> list)
        {

        }

        private void SaveAll() 
        {
            // Run this maybe once an day;
            foreach (var item in _myList.AsParallel())
            {
                Save(item);
            }
        }


        private void Save(T item)
        {
            
                var exist = _myOriginalList.FirstOrDefault(x => x.Id == item.Id && x.GuidId == item.GuidId.ToString());
                if (exist == null)
                    repository.Insert(item);
                else if (JsonFormatter.ToJson<T>(item) != JsonFormatter.ToJson<T>(exist))
                    repository.Update(item);            
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            Save(entity);
        }


        public T GetById(int Id)
        {
            return _myList.FirstOrDefault(x => x.Id == Id);
        }

        public void Insert(T entity)
        {
            var exist = _myList.FirstOrDefault(x => x.Id == entity.Id && x.GuidId == entity.GuidId.ToString());
            if (exist == null)
                _myList.Add(entity);
            else
            {
                var msg = $"Cannot insert entity {entity.GetType().Name} with id {entity.Id}. Entity already exists ";
                throw new IOException(msg);
            }
            Save(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _myList;
        }

        public IEnumerable<T> GetByExpression(Expression<Func<T, bool>> expression)
        {
            return _myList.Where(x=> expression.Compile()(x)).ToList();
        }

        public void Update(T entity)
        {
            var exist = _myList.FirstOrDefault(x => x.Id == entity.Id && x.GuidId == entity.GuidId.ToString());
            if (exist != null)
                _myList.Remove(exist);
            _myList.Add(entity);
            Save(entity);
        }
    }
}
