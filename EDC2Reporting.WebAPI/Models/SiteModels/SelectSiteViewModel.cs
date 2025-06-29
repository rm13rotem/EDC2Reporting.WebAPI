using MainStaticMaintainableEntities.SiteAssembly;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Models.SiteModels
{
    public class SelectSiteViewModel
    {
        
        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public List<SelectListItem> Sites { get; set; }
    }
}
