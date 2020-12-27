using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public class ValidDateFormatter
    {
        // Valid dates -= between 01Jan1900 and NOW.
        public static string SerializeToDaysFrom01Jan1970(DateTime date)
        {
            var startDate = new DateTime(1970, 1, 1);
            if (date < new DateTime(1900, 1, 1))
                return null; // error;
            if (date > DateTime.Now.Date.AddDays(1)) 
                return null; /// error
            int result = (date - startDate).Days;
            return result.ToString();
        }


        public static DateTime? DeserializeFrom_N_Days(string n_days)
        {
            if (!int.TryParse(n_days, out int n_days_int))
                return null;
            var startDate = new DateTime(1970, 1, 1);
            DateTime result = startDate.AddDays(n_days_int);

            if (result < new DateTime(1900, 1, 1))
                return null; // error;
            if (result > DateTime.Now.AddDays(1))
                return null; /// error

            return result;
        }
    }
}
