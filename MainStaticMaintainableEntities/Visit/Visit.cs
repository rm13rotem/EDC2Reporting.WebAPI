using MainStaticMaintainableEntities.ModuleAssembly;
using System.Collections.Generic;

namespace MainStaticMaintainableEntities.VisitAssembly
{
    public class Visit : PersistantEntity
    {
        public List<Module> Modules { get; set; }
    }
}