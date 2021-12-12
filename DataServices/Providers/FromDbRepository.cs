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
            foreach (var persistentEntity in source)
            {
                T t = default(T);
                if (persistentEntity.EntityName == typeof(T).Name)
                {
                    t = JsonConvert.DeserializeObject<T>(persistentEntity.JsonValue);
                    dictionary.Add(persistentEntity, t);
                }
            }
        }

        private PersistentEntity UpdateJsonValueInPersistentEntity(T entity)
        {
            var dbEntity = dictionary.FirstOrDefault(x => x.Value.Id == entity.Id).Key;
            if (dbEntity == null) dbEntity = new PersistentEntity();
            dbEntity.JsonValue = JsonConvert.SerializeObject(entity);
            return dbEntity;
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            PersistentEntity dbEntity = UpdateJsonValueInPersistentEntity(entity);
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

        public void InsertUpdateOrUndelete(T entity)
        {
            entity.Id = GetNewId();

            var dbEntity = UpdateJsonValueInPersistentEntity(entity);
            if (dbEntity.Id == 0)
            {
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
            else
            {
                _dataContext.Attach<PersistentEntity>(dbEntity);
                dbEntity.IsDeleted = false;
                _dataContext.Entry<PersistentEntity>(dbEntity).State = EntityState.Modified;
                _dataContext.SaveChanges();
            }


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
            foreach (PersistentEntity persistentEntity in myEntities)
            {
                try
                {
                    T deserializedItem = JsonConvert.DeserializeObject<T>(persistentEntity.JsonValue);
                    if (deserializedItem != null)
                    {
                        dictionary.Add(persistentEntity, deserializedItem);
                        result.Add(deserializedItem);
                    }
                }
                catch (Exception)
                {
                    if (!persistentEntity.JsonValue.Contains("{"))
                    {
                        T t = new T();
                        t.Id = GetNewId();
                        t.Name = persistentEntity.JsonValue;
                        t.GuidId = Guid.NewGuid().ToString();
                        t.IsDeleted = persistentEntity.IsDeleted;
                        persistentEntity.JsonValue = JsonConvert.SerializeObject(t);
                        dictionary.Add(persistentEntity, t);
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
            _dataContext.SaveChanges();
        }
    }
}
