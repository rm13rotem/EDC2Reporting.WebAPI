using DataServices.Common.Attributes;

namespace EDC2Reporting.WebAPI.Models.LoginModels
{
    [SanitizeInput]
    public class FullLogin
    {
        public int Id { get; set; }
        public string GuidId { get; set; }

        public string ExistingRole { get; set; }
        public string salt1 { get; set; }
        public string ExistingPassword1 { get; set; }
        public string Salt2 { get; set; }
        public string Password2 { get; set; }
    }
}
