using EDC2Reporting.WebAPI.Models.SiteModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.ViewComponents
{
    public class SelectSiteViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(SelectSiteViewModel viewModel)
        {
            return View(viewModel); // Looks for Views/Shared/Components/SelectSite/Default.cshtml
        }
    }
}
