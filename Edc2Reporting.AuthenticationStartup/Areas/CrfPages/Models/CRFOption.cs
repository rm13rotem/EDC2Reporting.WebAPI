using Edc2Reporting.AuthenticationStartup.Areas.PersistentEntities.Models;

namespace Edc2Reporting.AuthenticationStartup.Areas.CrfPages.Models
{
    public class CrfOption : IPersistantEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set ; }
        public int CrfQuestionId { get; set; }  // Foreign key to CRFQuestion
    }
}