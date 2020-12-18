using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Models
{
    public class ModuleData : IPersistantEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ExperimentId { get; set; }
        [Required]
        public int VisitGroupId { get; set; }
        [Required]
        public int VisitId { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int ModuleId { get; set; }
        [Required]
        public string DataInJson { get; set; }

        public string CurrentDataInJson { get; set; }
        public int LastUpdator { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime CurrentLastUpdateDateTime { get; set; }
    }

}
