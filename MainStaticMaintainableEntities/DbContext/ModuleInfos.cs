using System;
using System.Collections.Generic;

namespace MainStaticMaintainableEntities
{
    public partial class ModuleInfos
    {
        public int Id { get; set; }
        public int ExperimentId { get; set; }
        public int VisitGroupId { get; set; }
        public int VisitId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int ModuleId { get; set; }
        public string DataInJson { get; set; }
        public string CurrentDataInJson { get; set; }
        public int LastUpdator { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        public DateTime CurrentLastUpdateDateTime { get; set; }
    }
}
