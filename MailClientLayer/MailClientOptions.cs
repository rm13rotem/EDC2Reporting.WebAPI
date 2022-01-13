namespace MailClientLayer
{
    public class MailClientOptions
    {
        public static readonly string MailClientSettings = "MailClientSettings";

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }

    }
}