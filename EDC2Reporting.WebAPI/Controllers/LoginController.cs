using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EDC2Reporting.WebAPI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult FirstLogin()
        {
            // hardest - the URL with Id, GuidId, existingRole, DefaultPassword 1234
            // assigns user the quicklookId
            return View();
        }

        public IActionResult Login() 
        {
            // usual - quicklookId, username, and password
            // validates details - BirthDate, DoctorNumber, etc.
            // generates quickpassword


            return View();
        }

        public IActionResult QuickLogin()
        {
            // just the quicklookId, the yearOfBirth and a quickpassword
            return View();
        }
    }
}
