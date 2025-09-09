using System;
using System.Collections.Generic;
using System.Text;

namespace DataServices.SqlServerRepository.Models.VisitAssembley
{
    interface ILoadableByVisityGroupIds
    {
        void LoadFromDbByIds(List<int> VisitIds, List<int> VisitGroupIds);
    }
}
