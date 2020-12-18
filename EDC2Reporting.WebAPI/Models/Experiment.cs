using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Models
{
    public class Experiment : IPersistantEntity, IHasName
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName ="nvarchar(1000)")]
        public string GuidId { get; set; } //e.g. FBG_001_US - helsinki name

        [Column(TypeName = "nvarchar(1000)")]
        public string HelsinkiApprovalNumber { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string CompanyName { get; set; } // exact. e.g. "Marzevit Plastics 1989 Inc."
        public int? CompanyId { get; set; }
        public string Name { get; set; }  // "Teva experiment" (loose nickname)
    }
}
