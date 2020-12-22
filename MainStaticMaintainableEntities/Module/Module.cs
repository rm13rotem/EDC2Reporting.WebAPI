using Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainStaticMaintainableEntities.ModuleAssembly
{
    public class Module : PersistantEntity
    {
        public List<AbstractComponent> Components { get; set; }

    }
}
