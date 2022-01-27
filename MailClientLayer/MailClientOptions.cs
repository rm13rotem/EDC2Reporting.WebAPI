namespace MailClientLayer
{
    public class MailClientOptions
    {
        public static readonly string MailClientSettings = "MailClientSettings";

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }

        public bool MailAllUnsentFromDb { get; set; }
        public bool MailAllInRabbitMqQueue { get; set; }
        public string RabbitMqExchangeName { get; set; }
        public string RabbitMqQueueName { get; set; } // TODO - every message processed must
                                                      // be sent and logged.
    }
}