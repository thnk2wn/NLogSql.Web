using System;
using System.Net.Mail;
using EnsureThat;
using NLogSql.Web.Infrastructure.Settings;

namespace NLogSql.Web.Infrastructure.Messaging
{
    public class Mailer : IMailer
    {
        private static AppSettings.EmailSettings EmailSettings
        {
            get { return AppSettings.Default.Email; }
        }

        public void SendMail(string @from, string to, string subject, string body)
        {
            SendMail(new MailAddress(@from), to, subject, body);
        }

        public void SendMail(MailAddress from, string to, string subject, string body)
        {
            Ensure.That(to, "to").IsNotNullOrWhiteSpace();
            Ensure.That(subject, "subject").IsNotNullOrWhiteSpace();
            Ensure.That(body, "body").IsNotNullOrWhiteSpace();

            var client = new SmtpClient(EmailSettings.SmtpServer, EmailSettings.SmtpPort)
            {
                Credentials = EmailSettings.Credentials,
                EnableSsl = EmailSettings.EnableSSL
            };

            var msg = new MailMessage(from, new MailAddress(to))
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            // SendAsync?
            client.Send(msg);
        }

        public void SendMail(string to, string subject, string body)
        {
            SendMail(EmailSettings.SystemFromAddress, to, subject, body);
        }
    }
}