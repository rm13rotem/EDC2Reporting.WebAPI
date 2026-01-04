using DataServices.Common.Attributes;

namespace EDC2Reporting.WebAPI.Models.SiteModels
{
    [SanitizeInput]
    public class CountryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
