using DataServices.Interfaces;
using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataServices.Providers
{
    public class FromDbRepository<T> : IRepository<T>
        where T : IPersistentEntity, new()
    {
        private readonly EdcDbContext _dataContext;
        Dictionary<PersistentEntity, T> dictionary = new Dictionary<PersistentEntity, T>();

        public FromDbRepository(EdcDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public FromDbRepository(IEnumerable<PersistentEntity> source)
        {
            foreach (var item in source)
            {
                T t = default(T);
                if (item.EntityName == typeof(T).Name)
                {
                    t = JsonConvert.DeserializeObject<T>(item.JsonValue);
                    dictionary.Add(item, t);
                }
            }
        }

        private PersistentEntity GetPersistentEntityFromDictionary(T entity)
        {
            var dbEntity = dictionary.First(x => x.Value.Id == entity.Id).Key;
            dbEntity.JsonValue = JsonConvert.SerializeObject(entity);
            return dbEntity;
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            PersistentEntity dbEntity = GetPersistentEntityFromDictionary(entity);
            dbEntity.IsDeleted = true;

            _dataContext.PersistentEntity.Attach(dbEntity);
            _dataContext.Entry<PersistentEntity>(dbEntity).State = EntityState.Modified;
            _dataContext.SaveChanges();
        }

        public T GetById(int Id)
        {
            if (dictionary == null || dictionary.Count == 0)
                GetAll(); // from db;
            T result = dictionary.FirstOrDefault(x => x.Value.Id == Id).Value;
            return result;
        }

        public void Insert(T entity)
        {
            entity.Id = GetNewId();

            var dbEntity = GetPersistentEntityFromDictionary(entity);
            dbEntity.Id = _dataContext.Set<PersistentEntity>().Max(x => x.Id) + 1; //EF will override anyway;
            dbEntity.IsDeleted = false;
            dbEntity.GuidId = Guid.NewGuid().ToString();
            dbEntity.CreateDate = DateTime.UtcNow;
            dbEntity.EntityName = typeof(T).Name;
            dbEntity.Name = entity.Name;

            _dataContext.Set<PersistentEntity>().Add(dbEntity);
            _dataContext.SaveChanges();
            dictionary.Add(dbEntity, entity);
        }

        private int GetNewId()
        {
            IEnumerable<T> allExisting;
            if (dictionary == null || dictionary.Count == 0)
                allExisting = GetAll();
            else allExisting = dictionary.Values.ToList();
            if (allExisting.Count() > 0)
                return allExisting.Max(x => x.Id) + 1;
            else return 1;
        }

        public IEnumerable<T> GetAll(bool isForceRefresh = false)
        {
            if (!isForceRefresh && dictionary.Count > 0)
                return dictionary.Values.ToList();
            // else
            List<T> result = new List<T>();
            List<PersistentEntity> myEntities = _dataContext.Set<PersistentEntity>().
                Where(x => x.EntityName == typeof(T).Name).ToList(); // e.g. Sites only!
            foreach (PersistentEntity item in myEntities)
            {
                try
                {
                    T deserializedItem = JsonConvert.DeserializeObject<T>(item.JsonValue);
                    if (deserializedItem != null)
                    {
                        dictionary.Add(item, deserializedItem);
                        result.Add(deserializedItem);
                    }
                }
                catch (Exception)
                {
                    if (!item.JsonValue.Contains("{"))
                    {
                        T t = new T();
                        if (result.Count == 0) t.Id = 1;
                        else t.Id = result.Max(x => x.Id);
                        t.Name = item.JsonValue;
                        t.GuidId = Guid.NewGuid().ToString();
                        t.IsDeleted = item.IsDeleted;
                        item.JsonValue = JsonConvert.SerializeObject(t);
                        dictionary.Add(item, t);
                        result.Add(t);
                        _dataContext.SaveChanges();
                    }
                    continue;
                }

            }
            return result;
        }


        public void Update(T entity)
        {
            if (dictionary == null || dictionary.Count == 0)
                GetAll();
            var pair = dictionary.Single(x => x.Value.Id == entity.Id);
            var newValue = JsonConvert.SerializeObject(entity);
            if (pair.Key.JsonValue == newValue)
                return; // nothing here to update;
            pair.Key.JsonValue = newValue;

            _dataContext.Attach(pair.Key);
            _dataContext.Entry<PersistentEntity>(pair.Key).State = EntityState.Modified;
            _dataContext.SaveChanges();
        }
    }
}
