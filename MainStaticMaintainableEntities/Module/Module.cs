using Components;
using DataServices.Interfaces;
using System.Collections.Generic;

namespace MainStaticMaintainableEntities.ModuleAssembly
{
    public class Module : IPersistentEntity
    {
        public List<AbstractComponent> Components { get; set; }


        public int Id { get; set; }
        public string GuidId { get; set; }
        public bool IsDeleted { get; set; }
        public string JsonValue { get; set; }
        public string Name { get; set; }
    }
}
