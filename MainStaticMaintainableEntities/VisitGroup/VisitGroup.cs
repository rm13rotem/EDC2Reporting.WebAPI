using System;
using System.Collections.Generic;
using System.Text;

namespace MainStaticMaintainableEntities.VisitGroup
{
    public class VisitGroup : PersistantEntity
    {
        public List<VisitGroup> VisitGroups { get; set; }
        public List<Visit> Visits { get; set; }
    }
}
