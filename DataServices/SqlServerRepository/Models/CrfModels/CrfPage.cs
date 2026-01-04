using DataServices.Common.Attributes;
using DataServices.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataServices.SqlServerRepository.Models.CrfModels
{
    [SanitizeInput]
    public class CrfPage : IPersistentEntity
    {
        [Key]
        public int Id { get; set; }
        public int StudyId { get; set; }  // Foreign key to Study

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Html { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsLockedForChanges { get; set; } = false;


        // Navigation: one CrfPage can have many CrfEntries
        public ICollection<CrfEntry> Entries { get; set; } = new List<CrfEntry>();
        public string GuidId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
