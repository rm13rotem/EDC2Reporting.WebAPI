using System;
using System.Collections.Generic;
using System.Text;

namespace MainStaticMaintainableEntities.Module
{
    public class Module : PersistantEntity
    {
        public List<Component> Components { get; set; }
    }
}
