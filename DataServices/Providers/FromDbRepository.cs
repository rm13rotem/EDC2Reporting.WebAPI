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
        private static IEnumerable<PersistentEntity> persistentEntities;
        Dictionary<PersistentEntity, T> dictionary = new Dictionary<PersistentEntity, T>();

        public FromDbRepository(EdcDbContext dataContext)
        {
            _dataContext = dataContext;
            persistentEntities = dataContext.PersistentEntities.ToList();
            LoadEntitiesToDictionaryFromSource(persistentEntities);
        }

        public FromDbRepository(IEnumerable<PersistentEntity> source)
        {
            persistentEntities = source;
            LoadEntitiesToDictionaryFromSource(source);
        }

        private void LoadEntitiesToDictionaryFromSource(IEnumerable<PersistentEntity> source)
        {
            foreach (var persistentEntity in source)
            {
                T t = default(T);
                if (persistentEntity.EntityName == typeof(T).Name)
                {
                    t = JsonConvert.DeserializeObject<T>(persistentEntity.JsonValue);
                    if (t != null)
                        dictionary.Add(persistentEntity, t);
                }
            }
        }

        private PersistentEntity GetDbEntityOrDefault(T entity)
        {
            if (dictionary == null || dictionary.Count == 0)
                GetAll();

            var dbEntity = dictionary.FirstOrDefault(x => x.Value.Id == entity.Id).Key;

            if (dbEntity != null)
                return dbEntity;
            //else 
            dbEntity = new PersistentEntity()
            {
                CreateDate = DateTime.UtcNow,
                EntityName = typeof(T).Name,
                GuidId = Guid.NewGuid().ToString(),
                IsDeleted = false,
                Name = entity.Name
            }
            ;
            if (_dataContext != null)
            {
                // No need for the following line - Identity Insert
                //dbEntity.Id = _dataContext.PersistentEntities.Max(x => x.Id) + 1;
                //db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT PersistentEntity ON");
                //db.PersistentEntities.Add(dbEntity);
                //db.SaveChanges();
                //db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT PersistentEntity OFF");


                if (dbEntity.JsonValue == null)
                    dbEntity.JsonValue = JsonConvert.SerializeObject(entity);
                _dataContext.PersistentEntities.Add(dbEntity);
                _dataContext.SaveChanges();
                dictionary.Add(dbEntity, entity);
            }
            return dbEntity;
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            PersistentEntity dbEntity = GetDbEntityOrDefault(entity);
            dbEntity.JsonValue = JsonConvert.SerializeObject(entity);
            dbEntity.IsDeleted = true;

            _dataContext.PersistentEntities.Attach(dbEntity);
            _dataContext.Entry<PersistentEntity>(dbEntity).State = EntityState.Modified;
            _dataContext.SaveChanges();
        }

        public T GetById(int Id)
        {
            T result = default(T);
            if (dictionary == null || dictionary.Count == 0)
                return result;

            result = dictionary.FirstOrDefault(x => x.Value.Id == Id).Value;
            return result;
        }

        public void UpsertActivation(T entity)
        {
            if (entity.Id == 0)
                entity.Id = GetNewId();

            var dbEntity = GetDbEntityOrDefault(entity);

            _dataContext.Attach<PersistentEntity>(dbEntity);
            dbEntity.IsDeleted = false;
            _dataContext.Entry<PersistentEntity>(dbEntity).State = EntityState.Modified;
            _dataContext.SaveChanges();
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
            if (!isForceRefresh && dictionary != null && dictionary.Count > 0)
                return dictionary.Values.ToList();
            // else
            List<T> result = new List<T>();
            List<PersistentEntity> myEntities = _dataContext.PersistentEntities
                .Where(x => x.EntityName.ToLower() == typeof(T).Name.ToLower()).ToList(); // e.g. Sites only!
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
            var pair = dictionary.FirstOrDefault(x => x.Value.GuidId == entity.GuidId || x.Value.Id == entity.Id);
            if (pair.Key == null)
                return;
            var newValue = JsonConvert.SerializeObject(entity);
            if (pair.Key.JsonValue == newValue)
                return; // nothing here to update;
            pair.Key.JsonValue = newValue;

            _dataContext.Attach(pair.Key);
            _dataContext.SaveChanges();
        }
    }
}
