using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Edc2Reporting.AuthenticationStartup.Areas.CrfPages.Models
{
    public class CrfEntry
    {
        [Key]
        public int Id { get; set; }


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
