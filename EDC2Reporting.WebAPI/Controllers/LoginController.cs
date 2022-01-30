using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDC2Reporting.WebAPI.Models.LoginModels;
using Microsoft.AspNetCore.Mvc;
using SessionLayer;

namespace EDC2Reporting.WebAPI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISessionWrapper _sessionWrapper;

        public LoginController(ISessionWrapper sessionWrapper)
        {
            _sessionWrapper = sessionWrapper;
        }
        public IActionResult FirstLogin(FullLogin model)
        {
            // hardest - the URL with Id, GuidId, existingRole, DefaultPassword 1234
            // assigns user the quicklookId
            
            //if (roleProvider.IsValid(model)) ...
            // CreateSession....
            return View(model);
        }

        public IActionResult Login(LoginViewModel loginViewModel) 
        {
            // usual - quicklookId, username, and password
            // validates details - BirthDate, DoctorNumber, etc.
            // generates quickpassword

            //if (roleProvider.IsValid(model)) ...
            //   CreateSession...
            return View();
        }

        public IActionResult QuickLogin(QuickLoginViewModel model)
        {
            // just the quicklookId, the yearOfBirth and a quickpassword

            //if (roleProvider.IsValid(model))
            //    Continue Session until a maximum of 3 days.
            return View();
        }
    }
}
