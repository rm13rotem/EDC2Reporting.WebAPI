using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Models.LoginModels
{
    public class Investigator : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DoctorNumber { get; set; }
        public string QuickLookId { get; set; }
    }
}
