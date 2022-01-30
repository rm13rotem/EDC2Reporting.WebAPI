using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Models.LoginModels
{
    public class LoginViewModel
    {
        public string QuickLookId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
