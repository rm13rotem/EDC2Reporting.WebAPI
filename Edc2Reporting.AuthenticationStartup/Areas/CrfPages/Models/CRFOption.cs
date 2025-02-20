namespace Edc2Reporting.AuthenticationStartup.Areas.CrfPages.Models
{
    public class CrfOption
    {
        public int CRFOptionId { get; set; }
        public int CrfQuestionId { get; set; }  // Foreign key to CRFQuestion
        public string OptionText { get; set; }

        public int CRFQuestionId { get; set; }
    }
}