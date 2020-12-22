using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI
{
    /// <summary>

    /// Custom Exception for serialization

    /// </summary>
    public class EDC_Exception
    {

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public EDC_Exception InnerException { get; set; }

        /// <summary>

        /// Create new Exception

        /// </summary>

        /// <param name="ex">Exception to convert</param>

        public EDC_Exception(Exception ex)

        {

            Message = ex.Message;

            StackTrace = ex.StackTrace;

            if (ex.InnerException != null)

            {

                InnerException = new EDC_Exception(ex.InnerException);

            }

        }

        /// <summary>

        /// Parameterless constructor for serialization

        /// </summary>

        public EDC_Exception() { }


    }
}
