namespace DataServices.SqlServerRepository.Models
{
    public class EmailModel : IEmailModel
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string BCC { get; set; }

        public string CC { get; set; }

        public bool IsBodyHtml { get; set; } = true;
        public string Attachment { get; set; }
        public bool IsSent { get; set; } = false;

    }
}
