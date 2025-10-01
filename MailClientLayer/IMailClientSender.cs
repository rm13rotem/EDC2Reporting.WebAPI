using DataServices.SqlServerRepository.Models;

namespace MailClientLayer
{
    public interface IMailClientSender
    {
        bool TryInsertIntoQueue(EmailModel email);
        bool SendAllPendingEmails();
    }
}