using System;
using System.Collections.Generic;
using System.Text;

namespace MainStaticMaintainableEntities.Interfaces
{
    interface ILoadableByVisityGroupIds
    {
        void LoadFromDbByIds(List<int> VisitIds, List<int> VisitGroupIds);
    }
}
