using DataServices.Common.Attributes;

namespace EDC2Reporting.WebAPI.Models.LoginModels
{
    [SanitizeInput]
    public class QuickLoginViewModel
    {
        public string QuickLookId { get; set; }
        public int YearOfBirth { get; set; }
        public string ShortPassword { get; set; }
    }
}
