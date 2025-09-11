using DataServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataServices.SqlServerRepository.Models
{
    public class Patient : IPersistentEntity
    {
        public int SubjectIdInTrial { get; set; }
        public int StudyId { get; set; }
        public string SubjectInitials { get; set; } // 3 letters
        public int Id { get; set; }
        public string GuidId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string Name { get; set; } = string.Empty;
    }
}
