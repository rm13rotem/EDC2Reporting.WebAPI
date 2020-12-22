using System;
using System.Collections.Generic;
using System.Text;

namespace MainStaticMaintainableEntities.SiteAssembly
{
    public class Site : PersistantEntity
    {
        public Doctor SiteManager { get; set; }
    }
}
