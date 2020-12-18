using System;
using System.Collections.Generic;
using System.Text;

namespace MainStaticMaintainableEntities.PatientAssembley
{
    public class Patient : PersistantEntity
    {
        public int SubjectIdInTrial { get; set; }
        public int ExperimentId { get; set; }
    }
}
