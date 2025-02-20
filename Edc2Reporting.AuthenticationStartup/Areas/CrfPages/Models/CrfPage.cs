using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edc2Reporting.AuthenticationStartup.Areas.CrfPages.Models
{
    public class CrfPage
    {
        public int CRFPageId { get; set; }
        public int StudyId { get; set; }  // Foreign key to Study
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation property to Questions
        public ICollection<CrfPageComponent> Questions { get; set; }
    }
}
