using DataServices.Common.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EDC2Reporting.WebAPI.Models.SiteModels
{
    [SanitizeInput]
    public class SelectSiteViewModel
    {
        
        public List<SelectListItem> CountryId { get; set; }
        public List<SelectListItem> CityId { get; set; }
        public List<SelectListItem> SiteId { get; set; }
    }
}
