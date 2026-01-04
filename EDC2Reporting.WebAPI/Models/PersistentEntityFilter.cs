using DataServices.Common.Attributes;
using System;

namespace EDC2Reporting.WebAPI.Models
{

    [SanitizeInput]
    public class PersistentEntityFilter
    {
        public string EntityName { get; set; } = string.Empty;
        public bool IsDeletedDisplayed { get; set; } = false;

        public DateTime CreatedFrom { get; set; }
    }
}
