using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edc2Reporting.AuthenticationStartup.Areas.CrfPages.Models
{
    public class CrfAnswer
    {
        public int Id { get; set; }
        public int CRFQuestionId { get; set; }
        public int PatientId { get; set; }  // Foreign key to Patient
        public string AnswerText { get; set; }  // For text answers
        public int? OptionId { get; set; }  // For multiple-choice answers (nullable)
        public DateTime? AnswerDate { get; set; }  // For date questions
        public double? AnswerNumeric { get; set; }  // For numeric answerpublic bool? CheckboxAnswer { get; set; }  // For checkbox questions

        // Navigation properties
        public int CrfQuestionId { get; set; }
        public Patient Patient { get; set; }
        public CRFOption CRFOption { get; set; }
    }
}
