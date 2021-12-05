using DataServices.Interfaces;
using MainStaticMaintainableEntities.SiteAssembly;
using System;

namespace MainStaticMaintainableEntities
{
    public class Doctor : IPersistentEntity
    {
        public DateTime DateOfBirth { get; set; }
        public int DoctorNumber { get; set; }

        public virtual Site Site { get; set; }

        public int Id { get; set; }
        public string GuidId { get; set; }
        public bool IsDeleted { get; set; }
        public string JsonValue { get; set; }
        public string Name { get; set; }
    }
}
