using System;

namespace EDC2Reporting.WebAPI.Models
{
    public class PersistentEntityFilter
    {
        public string EntityName { get; set; } = string.Empty;
        public bool IsDeletedDisplayed { get; set; } = false;

        public DateTime CreatedFrom { get; set; }
    }
}
