using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edc2Reporting.AuthenticationStartup.Areas.CrfPages.Models
{
    public class CrfAnswer
    {
        public int Id { get; set; }


        /// <summary>
        /// Foreign key to Doctor filling out the form
        /// </summary>
        public int DoctorId { get; set; }
        /// <summary>
        /// Foreign key to Patient (101, ...)
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Foreign key to Visit type (Visit Id)
        /// </summary>
        public int VisitId { get; set; }
        /// <summary>
        /// Foreign key to Visit Index inside the big VisitGroup
        /// </summary>        
        public int VisitIndex { get; set; }
        /// <summary>
        /// Study Id being done
        /// </summary>       
        public int StudyId { get; set; }
        public QuestionType AnswerType { get; set; }
        public string AnswerInText { get; set; } = "";  // For all answers
        public string AnswerText { get; set; } = "";  // For text answers
        public int? OptionId { get; set; }  // For multiple-choice answers (nullable)
        public DateTime? AnswerDate { get; set; }  // For date questions
        public double? AnswerNumeric { get; set; }  // For numeric answerpublic bool?
        public bool? CheckboxAnswer { get; set; }  // For checkbox questions

        /// <summary>
        // Navigation properties
        /// Origin of the answer
        /// </summary>
        public int CrfQuestionId { get; set; }
        public CrfOption CRFOption { get; set; }
    }
}
