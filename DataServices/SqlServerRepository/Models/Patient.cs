using System;
using System.Collections.Generic;
using System.Text;

namespace DataServices.SqlServerRepository.Models
{
    public class Patient : PersistentEntity
    {
        public int SubjectIdInTrial { get; set; }
        public int StudyId { get; set; }
        public string SubjectInitials { get; set; } // 3 letters
    }
}
