using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edc2Reporting.AuthenticationStartup.Areas.CrfPages.Models
{
    public class CrfPage
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

    }
}
