using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataServices.SqlServerRepository.Models
{
    public partial class ModuleInfo
    {
        [Key]
        public int Id { get; set; }
        public int ExperimentId { get; set; }
        public int VisitGroupId { get; set; }
        public int VisitId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int ModuleId { get; set; }
        [Required]
        public string DataInJson { get; set; }
        public string CurrentDataInJson { get; set; }
        public int LastUpdator { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CurrentLastUpdateDateTime { get; set; }
    }
}
