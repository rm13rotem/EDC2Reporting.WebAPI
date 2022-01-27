using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Models
{
    public class ThreatsModelSingleton

    {
        private static ThreatsModelSingleton _instance;

        public static ThreatsModelSingleton GetInstance()
        {
            if (_instance != null) return _instance;
            // else
            _instance = new ThreatsModelSingleton();
            return _instance;
        }
        private ThreatsModelSingleton()
        {
            ControllerUsage = new RegisteredControllerUsage();
        }
        public DateTime LastDayChecked { get; set; }
        // Should always be 1, and if not SendUrgentEmail
        public int NumberOfAdminsOnSite { get; set; }
        public int NumberOfActiveAdminsOnline { get; set; }
        public string Message { get; set; } // Is ToString() enough?

        public int NumberOfDailyEntries { get; set; } // From Session;
        public int NumberOfDailyFailedAttempts { get; set; }
        public int NumberOfLogLines { get; set; }
        public int NumberOfErrorsToday { get; set; }

        public int NumberOfFailedLoginsToday { get; set; }
        public RegisteredControllerUsage ControllerUsage { get; internal set; }
    }
}
