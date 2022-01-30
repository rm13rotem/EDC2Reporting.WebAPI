using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Models.LoginModels
{
    public class QuickLoginViewModel
    {
        public string QuickLookId { get; set; }
        public int YearOfBirth { get; set; }
        public string ShortPassword { get; set; }
    }
}
