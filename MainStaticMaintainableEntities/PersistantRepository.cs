using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace MainStaticMaintainableEntities
{
    public class PersistantRepository
    {
        public List<T> GetAll<T>(List<PersistantEntity> list)
            where T : class
        {
            return list.Where(x => x.EntityName.ToLower() == typeof(T).ToString().ToLower())
                .Select(x=>JsonFormatter.Deserialize<T>(x.JsonValue)).ToList();
        }
    }
}
