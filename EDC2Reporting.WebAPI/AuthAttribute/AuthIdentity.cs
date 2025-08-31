using System.Security.Principal;


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
