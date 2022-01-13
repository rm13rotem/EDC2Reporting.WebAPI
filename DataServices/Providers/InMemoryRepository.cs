using DataServices.Interfaces;
using DataServices.SqlServerRepository.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Utilities;

namespace DataServices.Providers
{
    public class InMemoryRepository<T> : IRepository<T> where T : IPersistentEntity
    {
        // Todo - add additional functionality to the speicific experiment
        public IRepository<T> repository { get; set; }
        private List<T> _myList;

        public InMemoryRepository(IRepository<T> _repository)
        {
            repository = _repository;
            _myList = repository.GetAll().ToList();
        }

        public InMemoryRepository(List<T> list)
        {
            _myList = list;
            if (list == null)
                _myList = new List<T>();
        }

        public void Delete(T entity)
        {
            repository.Delete(entity);
            var exist = _myList.FirstOrDefault(x => x.Id == entity.Id && x.GuidId == entity.GuidId.ToString());
            if (exist != null)
                _myList.Remove(exist);
        }


        public T GetById(int Id)
        {
            return _myList.FirstOrDefault(x => x.Id == Id);
        }

        public void UpsertActivation(T entity)
        {
            if (entity == null)
                return;

            var exist = _myList.FirstOrDefault(x => x.Id == entity.Id && x.GuidId == entity.GuidId.ToString());
            if (exist == null)
            {
                _myList.Add(entity);
                if (repository != null)
                    repository.UpsertActivation(entity);
            }
            else
            {
                if (exist != null && entity.GuidId != null && entity.Id > 0)
                {
                    ReplaceExistingWithEntity(exist, entity);
                }
                else if (exist != null)
                {
                    if (string.IsNullOrWhiteSpace(entity.GuidId))
                        entity.GuidId = Guid.NewGuid().ToString();
                    if (entity.Id == 0)
                    {
                        int maxId = 0;
                        if (_myList.Count > 0)
                            maxId = _myList.Max(x => x.Id) + 1;
                        else maxId = 1;
                        exist.Id = maxId;
                    }
                    ReplaceExistingWithEntity(exist, entity);
                }
                else
                {
                    var msg = $"Cannot insert entity {entity.GetType().Name} with id {entity.Id}. Entity already exists ";
                    throw new IOException(msg);
                }
            }
        }

        private void ReplaceExistingWithEntity(T exist, T entity)
        {
            _myList.Remove(exist);
            _myList.Add(entity);
            if (repository != null)
                repository.Update(entity);
        }

        public IEnumerable<T> GetAll(bool isForceReload = false)
        {
            if (!isForceReload)
                return _myList;
            else
            {
                if (repository != null)
                    _myList = repository.GetAll().ToList();
                else throw new IOException("External Repository not linked to the InMemoryRepository");
            }
            return _myList;
        }

        public IEnumerable<T> GetByExpression(Expression<Func<T, bool>> expression)
        {
            return _myList.Where(x => expression.Compile()(x)).ToList();
        }

        public void Update(T entity)
        {
            var exist = _myList.FirstOrDefault(x => x.Id == entity.Id || x.GuidId == entity.GuidId.ToString());
            if (exist != null)
                ReplaceExistingWithEntity(exist, entity);
            else throw new IOException($"Entity Id {entity.Id} , Name {entity.Name} does not exist");
        }
    }
}
