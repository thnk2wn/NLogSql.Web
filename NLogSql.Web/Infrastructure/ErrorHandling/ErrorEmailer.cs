using System;
using System.Reflection;
using EnsureThat;
using NLogSql.Web.Infrastructure.Diagnostics.Info;
using NLogSql.Web.Infrastructure.Diagnostics.Logging;
using NLogSql.Web.Infrastructure.Messaging;
using NLogSql.Web.Infrastructure.Settings;

namespace NLogSql.Web.Infrastructure.ErrorHandling
{
    public interface IErrorEmailer
    {
        void SendErrorEmail(ErrorReportInfo errorInfo);
    }

    public class ErrorEmailer : IErrorEmailer
    {
        private readonly IMailer _mailer;
        private readonly ILog _log;

        public ErrorEmailer(IMailer mailer, ILog log)
        {
            _mailer = Ensure.That(mailer, "mailer").IsNotNull().Value;
            _log = Ensure.That(log, "log").IsNotNull().Value;
        }

        public void SendErrorEmail(ErrorReportInfo errorInfo)
        {
            try
            {
                var subject = string.Format("{0} Error", Assembly.GetExecutingAssembly().GetName().Name);

                if (null != errorInfo.Server && null != errorInfo.Location
                    && !string.IsNullOrWhiteSpace(errorInfo.Location.ControllerAction)
                    && !string.IsNullOrWhiteSpace(errorInfo.Server.HostName))
                {
                    subject = string.Format("{0}: {1} - {2}", subject, errorInfo.Server.HostName,
                                            errorInfo.Location.ControllerAction);
                }

                var to = AppSettings.Default.Email.ErrorMessageTo;
                _mailer.SendMail(to, subject, errorInfo.ReportHtml);

                _log.Info("Sent email: {0} to {1}", subject, to);
            }
            catch (Exception ex)
            {
                _log.Error("Error sending error report email: {0}", ex);
            }
        }
    }
}