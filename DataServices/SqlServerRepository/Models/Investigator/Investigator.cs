using DataServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace DataServices.SqlServerRepository.Models
{
    public class Investigator : IdentityUser//, IPersistentEntity, IInvestigator
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DoctorNumber { get; set; }
        public string QuickLookId { get; set; }


        // public DateTime DateOfBirth { get; set; }
        // public int SiteId { get; set; } // FK to SiteRepository

        // public virtual Site Site { get; set; }

        //public int Id { get; set; }
        // public string GuidId { get; set; }
        // public bool IsDeleted { get; set; }
        // public string JsonValue { get; set; }
        public string FullName => $"{FirstName} {LastName}".Trim();

    }
}
