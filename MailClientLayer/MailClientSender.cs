using DataServices.Interfaces;
using DataServices.Providers;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MailClientLayer
{
    public class MailClientSender : IMailClientSender
    {
        private readonly MailClientOptions mailOptions;
        private readonly IRepository<LoggedMailMessage> repository;

        public MailClientSender(
            IOptionsSnapshot<MailClientOptions> mailClientOptionsSnapshot,
            IOptionsSnapshot<RepositoryOptions> options)
        {
            mailOptions = mailClientOptionsSnapshot.Value;
            var dbOptions = options.Value;
            DbRepositoryLocator<LoggedMailMessage> dbRepositoryLocator = new DbRepositoryLocator<LoggedMailMessage>();
            repository = dbRepositoryLocator.GetRepository();
        }

        public bool TryInsertIntoQueue(LoggedMailMessage message)
        {
            try
            {
                int EmailMessageId = LogToDb(message);
                if (EmailMessageId <= 0)
                    return false;
                // else 
                TrySendEmail(EmailMessageId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<bool> TrySendAllUnsentEmailsAsync()
        {
            bool result = true;
            var toBeSent = repository.GetAll().Where(x => x.IsDeleted = false);
            foreach (var msg in toBeSent.AsParallel())
            {
                bool success = await TrySendEmailAsync(msg);
                if (!success)
                    result = false;
            }
            return result;
        }

        private async Task<bool> TrySendEmailAsync(LoggedMailMessage msg)
        {
            try
            {
                var smtp = SmtpFactory.GetSmtp(mailOptions);
                MailMessage mailMessage = MailMessageFactory.GetMailMessage(msg, mailOptions);
                smtp.Send(mailMessage);

                msg.IsDeleted = true;
                await Task.Run(() => repository.Update(msg));
                return true;
            }
            catch (Exception)
            {
                msg.IsDeleted = false;
                await Task.Run(() => repository.Update(msg));
                return false;
            }
        }

        private bool TrySendEmail(int emailMessageId)
        {
            var message = repository.GetById(emailMessageId);
            return TrySendEmailAsync(message).Result;
        }

        private int LogToDb(LoggedMailMessage message)
        {
            try
            {
                repository.UpsertActivation(message);
                return message.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
