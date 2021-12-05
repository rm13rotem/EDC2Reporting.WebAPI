using System;
using System.Collections.Generic;

namespace MainStaticMaintainableEntities
{
    public partial class Experiments
    {
        public int Id { get; set; }
        public string UniqueIdentifier { get; set; }
        public string HelsinkiApprovalNumber { get; set; }
        public string CompanyName { get; set; }
        public int? CompanyId { get; set; }
        public string Name { get; set; }
    }
}
