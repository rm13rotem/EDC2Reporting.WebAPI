using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Net.Mail;

namespace MailClientLayer
{
    public class MailClientSender : IMailClientSender
    {
        private readonly MailClientOptions mailOptions;
        private readonly SmtpClient client;
        private readonly EdcDbContext _context;
        private readonly ILogger<MailClientSender> _logger;
        private static bool _isSending;

        public MailClientSender(EdcDbContext context,
            IOptionsSnapshot<MailClientOptions> mailClientOptionsSnapshot,
            ILogger<MailClientSender> logger)
        {
            // 1. Get options
            mailOptions = mailClientOptionsSnapshot.Value;

            // 2. Setup SMTP client. using mailOptions
            client = SmtpFactory.GetSmtp(mailOptions);

            // 3. DbContext
            _context = context;

            // 4. Logger
            _logger = logger;
        }

        public bool TryInsertIntoQueue(EmailModel email)
        {
            try
            {
                _context.Emails.Add(email);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to insert email into queue");
                return false;
            }
        }




        private bool SendEmail(EmailModel emailModel)
        {
            var mail = new MailMessage
            {
                From = new MailAddress("rm13rotem@gmail.com", "רותם מירון 972528829604"),
                Subject = emailModel.Subject,
                Body = emailModel.Body,
                IsBodyHtml = emailModel.IsBodyHtml
            };

            if (!string.IsNullOrWhiteSpace(emailModel.To))
            {
                AddToCollection(emailModel.To, mail.To);
            }
            if (!string.IsNullOrWhiteSpace(emailModel.CC))
            {
                AddToCollection(emailModel.CC, mail.CC);
            }
            if (!string.IsNullOrWhiteSpace(emailModel.BCC))
            {
                AddToCollection(emailModel.BCC, mail.Bcc);
            }

            if (!string.IsNullOrWhiteSpace(emailModel?.Attachment))
            {
                foreach (var attachmentStr in emailModel.Attachment.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string filePath = @"C:\Users\rm13r\Downloads\";
                    var allFiles = Directory.GetFiles(filePath);
                    
                    filePath = allFiles.FirstOrDefault(x=>x.Contains(attachmentStr));
                    if (filePath != null && File.Exists(filePath))
                    {
                        Attachment attachment = new Attachment(filePath);
                        mail.Attachments.Add(attachment);
                    }
                    else
                    {
                        Console.WriteLine($"Attachment not found: {filePath}");
                    }
                }
            }
            // 4. Send
            try
            {
                client.Send(mail);
                Console.WriteLine($"Email sent to {emailModel.To}");
                emailModel.IsSent = true;
                _context.Emails.Update(emailModel);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send to {emailModel.To}: {ex.Message}");
                emailModel.IsSent = false;
                _context.Emails.Update(emailModel);
                _context.SaveChanges();
                return false;
            }
        }

        private void AddToCollection(string adresses, MailAddressCollection mailProperty)
        {
            var toAddresses = adresses.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var toAddress in toAddresses)
            {
                var toAddress1 = toAddress.Replace(" ", "");
                mailProperty.Add(toAddress1);
            }
        }

        public bool SendAllPendingEmails()
        {
            if (_isSending)
            {
                _logger.LogWarning("SendAllPendingEmails is already running.");
                return true;
            }
            _isSending = true;
            try
            {
                var pending = _context.Emails.Where(e => !e.IsSent).ToList();
                bool allOk = true;

                foreach (var email in pending)
                {
                    try
                    {
                        bool isSuccess = SendEmail(email);
                        if (!isSuccess)
                        {
                            allOk = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Failed to send email to {email.To}");
                        allOk = false;
                    }
                }
                return allOk;
            }
            catch (Exception ex1)
            {
                _logger.LogError(ex1, $"Failed to send emails. Loop threw unhandled exception");
                return false;
            }
            finally
            {
                _isSending = false;
            }
        }
    }
}
