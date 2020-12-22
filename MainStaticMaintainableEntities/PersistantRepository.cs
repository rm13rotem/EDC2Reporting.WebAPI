using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace MainStaticMaintainableEntities
{
    public class PersistantRepository
    {
        public IEnumerable<T> GetAll<T>(List<PersistantEntity> list)
            where T : class
        {
            // TODO : try/catch and go to DB layer
            return list.Where(x => x.EntityName.ToLower() == typeof(T).ToString().ToLower())
                .Select(x=>JsonFormatter.FromJson<T>(x.JsonValue)).ToList();
        }

        public bool Upsert(PersistantEntity entity)
        {
            entity.EntityName = entity.GetType().Name.ToLower();
            string newJsonValue = JsonFormatter.ToJson(entity);
            if (newJsonValue == entity.JsonValue)
                return true; // nothing here to update;
            if (string.IsNullOrEmpty(newJsonValue))
                return false; // should never happen;
            //else
            // 1. Update Audit (if applicable and activated)

            // 2. Save to DB
            entity.JsonValue = newJsonValue;
            try
            {

            }
            catch (Exception ex)
            {
                // log exception;
            }
            return true;
        }
    }
}
