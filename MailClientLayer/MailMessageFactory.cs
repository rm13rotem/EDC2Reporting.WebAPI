using System;
using System.Net.Mail;

namespace MailClientLayer
{
    public class MailMessageFactory
    {
        public static MailMessage GetMailMessage(LoggedMailMessage msg, MailClientOptions mailOptions)
        {
            var fromAddress = new MailAddress(mailOptions.UserName, msg.From);
            var toAddress = new MailAddress(msg.ToEmail, msg.To);
            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = msg.Subject,
                Body = msg.Body
            };
            return message;
        }
    }
}