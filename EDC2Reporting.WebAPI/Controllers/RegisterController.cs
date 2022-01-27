using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EDC2Reporting.WebAPI.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult InviteAdministrator()
        {
            // If not administrator - goto home/error
            // notify by alert email all existing administrators

            // Send Mail
            return View();
        }

        public IActionResult InviteClinicalTrialLeader()
        {
            // If not administartor or CTL - go to home error
            // notify by alert email all existing CTLs

            // Send Mail
            return View();
        }

        public IActionResult InviteSiteManager()
        {
            // If not admin or CTL or Site Manager - got to home error

            // Send Mail
            return View();
        }
        public IActionResult InviteUser()
        {

            // Send Mail
            return View();
        }
        public IActionResult InviteAdministrator()
        {
            // notify by alert email all existing administrators
            return View();
        }
    }
}
