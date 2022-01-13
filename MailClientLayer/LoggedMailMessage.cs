using DataServices.Interfaces;

namespace MailClientLayer
{
    public class LoggedMailMessage : IPersistentEntity
    {
        public int Id { get; set; }
        public string GuidId { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string From { get; set; }
        public string ToEmail { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}