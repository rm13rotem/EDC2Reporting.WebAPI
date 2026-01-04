using DataServices.Common.Attributes;
using DataServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace DataServices.SqlServerRepository.Models
{
    [SanitizeInput]
    public class Investigator : IdentityUser //, IPersistentEntity, IInvestigator
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DoctorNumber { get; set; }
        public string QuickLookId { get; set; }

        // Uncommented Properties
        public DateTime DateOfBirth { get; set; }
        public int SiteId { get; set; } // FK to SiteRepository
        public int ExperimentId { get; set; }

        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
