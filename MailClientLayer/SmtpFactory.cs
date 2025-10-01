using System;
using System.Net;
using System.Net.Mail;
using System.Security;

namespace MailClientLayer
{
    public class SmtpFactory
    {
        public static SmtpClient GetSmtp(MailClientOptions mailOptions)
        {
            var smtp = new SmtpClient
            {
                Host = mailOptions.Host,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mailOptions.UserName, mailOptions.Password),
                Timeout = 20000
            };
            return smtp;
            
        }
    }
}