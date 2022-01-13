using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataServices.SqlServerRepository.Models
{
    public partial class Experiment
    {
        [Key]
        public int Id { get; set; }
        [StringLength(1000)]
        public string UniqueIdentifier { get; set; }
        [StringLength(1000)]
        public string HelsinkiApprovalNumber { get; set; }
        [StringLength(1000)]
        public string CompanyName { get; set; }
        public int? CompanyId { get; set; }
        public string Name { get; set; }
        public int? MainVisitGroupId { get; set; }
    }
}
