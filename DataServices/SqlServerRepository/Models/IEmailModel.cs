namespace DataServices.SqlServerRepository.Models
{
    public interface IEmailModel
    {
        string Attachment { get; set; }
        string BCC { get; set; }
        string Body { get; set; }
        string CC { get; set; }
        string From { get; set; }
        int Id { get; set; }
        bool IsBodyHtml { get; set; }
        bool IsSent { get; set; }
        string Subject { get; set; }
        string To { get; set; }
    }
}