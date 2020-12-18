using System.Collections.Generic;

namespace MainStaticMaintainableEntities.Visit
{
    public class Visit : PersistantEntity
    {
        public List<Module> Modules { get; set; }
    }
}