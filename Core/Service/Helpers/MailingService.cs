using MimeKit;
using Shared.Common;
using Shared.Dto;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;

using MailKit.Net.Smtp;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers
{
    public class MailingService : IMailingService
    {


        private readonly MailSettings _mailSettings;

        public MailingService(MailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }
        public async Task SendEmailAsync(Email email)
        {
            try
            {
                // Build the message
                var mail = new MimeMessage();
                mail.Subject = email.Subject;
                mail.From.Add(MailboxAddress.Parse(_mailSettings.Email));
                mail.To.Add(MailboxAddress.Parse(email.To));

                var builder = new BodyBuilder
                {
                    TextBody = email.PlainTextBody ?? email.Body,
                    HtmlBody = email.Body
                };

                mail.Body = builder.ToMessageBody();

                // Establish connection and send
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Email, _mailSettings.Password);

                smtp.Send(mail);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                // Log or rethrow with clear context
                throw new Exception($"❌ Failed to send email to {email.To}: {ex.Message}", ex);
            }
        }
    }
}