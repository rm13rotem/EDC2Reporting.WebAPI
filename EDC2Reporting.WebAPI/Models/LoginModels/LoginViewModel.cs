using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Models.LoginModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string QuickLookId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; internal set; }
        public string GuidId { get; internal set; }
    }
}
