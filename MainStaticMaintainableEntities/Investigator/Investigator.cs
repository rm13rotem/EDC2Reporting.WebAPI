using DataServices.Interfaces;
using MainStaticMaintainableEntities.SiteAssembly;
using System;

namespace MainStaticMaintainableEntities
{
    public class Investigator : IPersistentEntity, IInvestigator, IdentityUser
    {
        public DateTime DateOfBirth { get; set; }
        public int DoctorNumber { get; set; }
        public int SiteId { get; set; } // FK to SiteRepository

        public virtual Site Site { get; set; }

        public int Id { get; set; }
        public string GuidId { get; set; }
        public bool IsDeleted { get; set; }
        public string JsonValue { get; set; }
        public string Name { get; set; }
    }
}
