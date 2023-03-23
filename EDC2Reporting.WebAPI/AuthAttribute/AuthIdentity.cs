using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;


namespace EDC2Reporting.WebAPI.AuthAttribute
{
    public class AuthIdentity : GenericIdentity
    {
        public AuthIdentity(string name, string password) : base(name, "Basic")
        {
            this.Password = password;

        }

        public string Password { get; set; }
    }
}
