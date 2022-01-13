namespace MailClientLayer
{
    public interface IMailClientSender
    {
        bool TryInsertIntoQueue(LoggedMailMessage message);
    }
}