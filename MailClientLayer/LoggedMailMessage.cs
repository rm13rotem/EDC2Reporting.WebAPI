using DataServices.Interfaces;
using DataServices.SqlServerRepository.Models;
using System;

namespace MailClientLayer
{
    public class LoggedMailMessage : IEmailModel
    {
        private EmailModel model;

        public LoggedMailMessage(EmailModel model)
        {
            this.model = model; 
            this.Id = model.Id;
            this.IsSent = model.IsSent;
            this.To = model.To;
            this.From = model.From;
            this.Subject = model.Subject;
            this.Body = model.Body;
            this.BCC = model.BCC;
            this.CC = model.CC;
            this.IsBodyHtml = model.IsBodyHtml;
            this.Attachment = model.Attachment;
            this.CreatedAt = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public bool IsSent { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string BCC { get; set; }
        public string CC { get; set; }
        public bool IsBodyHtml { get; set; } = true;
        public string Attachment { get; set; }

        // Extra decorator info
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}