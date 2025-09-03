using DataServices.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataServices.SqlServerRepository.Models.CrfModels
{
    public class CrfEntry // : IPersistentEntity (missing GuidId)
    {
        [Key]
        public int Id { get; set; }
        // public string GuidId { get; set; }
        public string Name { get { return $"CRF Entry {Id}"; } set { return; } }
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Foreign key to Doctor filling out the form
        /// </summary>
        public int DoctorId { get; set; }
        /// <summary>
        /// Foreign key to Patient (101, ...)
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Foreign key to Visit type (Visit Id)
        /// </summary>
        public int VisitId { get; set; }
        /// <summary>
        /// Foreign key to Visit Index inside the big VisitGroup
        /// </summary>        
        public int VisitIndex { get; set; }
        /// <summary>
        /// Study Id being done
        /// </summary>       
        public int StudyId { get; set; }

        /// <summary>
        /// Foreign key to Doctor filling out the form
        /// </summary>
        [Required]
        public int CrfPageId { get; set; }

        [ForeignKey(nameof(CrfPageId))]
        public CrfPage CrfPage { get; set; } = null!;

        /// <summary>   
        /// store the submitted form data (as JSON for flexibility)
        /// </summary>  
        [Required]
        public string FormDataJson { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}