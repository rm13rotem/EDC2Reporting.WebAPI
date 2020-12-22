using System;
using System.Collections.Generic;
using System.Text;

namespace MainStaticMaintainableEntities.Site
{
    public class Site : PersistantEntity
    {
        public Doctor SiteManager { get; set; }
    }
}
