using DataServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainStaticMaintainableEntities.SiteAssembly
{
    public class Site : IPersistentEntity 
    {
        public Doctor SiteManager { get; set; }

        public int Id { get; set; }
        public string GuidId { get; set; }
        public bool IsDeleted { get; set; }
        public string JsonValue { get; set; }
        public string Name { get; set; }
    }
}
