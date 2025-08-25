using EDC2Reporting.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EDC2Reporting.WebAPI.Controllers
{
    public class DiagnosticsController : Controller
    {
        public ActionResult ThreatsModel()
        {
            ThreatsModelSingleton m = ThreatsModelSingleton.GetInstance();
            return View(m);
        }

        public ActionResult ControllerUsage() {
            ThreatsModelSingleton m = ThreatsModelSingleton.GetInstance();
            return View(m.ControllerUsage);

        }
    }
}
