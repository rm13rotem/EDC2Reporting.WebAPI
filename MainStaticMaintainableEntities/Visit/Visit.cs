using DataServices.Interfaces;
using MainStaticMaintainableEntities.ModuleAssembly;
using System.Collections.Generic;

namespace MainStaticMaintainableEntities.VisitAssembly
{
    public class Visit : IPersistentEntity
    {
        public List<Module> Modules { get; set; }
        public int InternalIndex { get; set; }

        public int Id { get; set; }
        public string GuidId { get; set; }
        public bool IsDeleted { get; set; }
        public string JsonValue { get; set; }
        public string Name { get; set; }

    }
}