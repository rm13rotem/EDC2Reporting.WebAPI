using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace MainStaticMaintainableEntities
{
    public class PersistantRepository
    {
        public IEnumerable<T> GetAll<T>()
            where T : PersistantEntity
        {
            List<PersistantEntity> list = new List<PersistantEntity>();
            // TODO : try/catch and go to DB layer

            // result
            return list.Where(x => x.EntityName.ToLower() == typeof(T).ToString().ToLower())
                .Select(x=>JsonFormatter.FromJson<T>(x.JsonValue)).ToList();
        }

        //public bool UpsertAll<T>(IEnumerable<T> entities)
        //     where T : PersistantEntity
        //{
        //    List<PersistantEntity> list = new List<PersistantEntity>();
        //    // TODO : try/catch and go to DB layer

        //    foreach (var item in entities.OrderBy(x=>x.id))
        //    {
        //        var exist = list.FirstOrDefault(entity => entity.Id == item.id && entity.EntityName = entity.GetType().Name.ToLower());
        //        if (exist == null && item.id == 0)
        //            item.id = list.Max(x => x.Id) + 1;
        //        if (exist == null)
        //        {
        //            list.Add(item);
        //        }
        //        else
        //        {
        //            string newJsonValue = JsonFormatter.ToJson(item);
        //            if (newJsonValue == item.JsonValue)
        //                return true; // nothing here to update;
        //            if (string.IsNullOrEmpty(newJsonValue))
        //                return false; // should never happen;
        //                              //else
        //                              // 1. Update Audit (if applicable and activated)
        //                              //if (ConfigurationManager.AppSettings["IsAuditActivated])
        //                              // { .... }

        //            // 2. Save to DB
        //            item.JsonValue = newJsonValue;
        //            try
        //            {

        //            }
        //            catch (Exception)
        //            {
        //                // log exception;
        //                throw;
        //            }

        //        }


        //    }
            
        //    return true;
        //}
    }
}
