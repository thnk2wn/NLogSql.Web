using System.Net.Mail;

namespace NLogSql.Web.Infrastructure.Messaging
{
    public interface IMailer
    {
        void SendMail(string from, string to, string subject, string body);
        void SendMail(MailAddress from, string to, string subject, string body);
        void SendMail(string to, string subject, string body);
    }
}
