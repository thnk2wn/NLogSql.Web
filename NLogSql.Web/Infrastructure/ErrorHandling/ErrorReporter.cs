using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EnsureThat;
using NLogSql.Web.Infrastructure.Diagnostics.Info;
using NLogSql.Web.Infrastructure.Diagnostics.Logging;

namespace NLogSql.Web.Infrastructure.ErrorHandling
{
    public class ErrorReporter : IErrorReporter
    {
        private readonly ILog _log;

        public ErrorReporter(ILog log)
        {
            _log = Ensure.That(log, "log").IsNotNull().Value;
        }

        private string CustomActivityMessage { get; set; }

        public void ReportException(ControllerContext controllerContext, Exception exception, string customActivityMessage = null)
        {
            this.CustomActivityMessage = customActivityMessage;
            ReportException(new ExceptionContext(controllerContext, exception));
        }

        public void ReportException(ExceptionContext exceptionContext)
        {
            var errorInfo = new ErrorReportInfo(exceptionContext, this.CustomActivityMessage);
            errorInfo.Generate();
            _log.Error("Unexpected error: {0}", errorInfo.ReportText);

            if (errorInfo.Errors.Any())
                _log.Error("Error generating error report. Original exception: {0}", exceptionContext.Exception);

            // sending mail can be a little slow, don't delay end user seeing error page
            Task.Factory.StartNew(
            state =>
            {
                var errorReport = (ErrorReportInfo)state;
                DependencyResolver.Current.GetService<IErrorEmailer>().SendErrorEmail(errorReport);
            },
            errorInfo).ContinueWith(t =>
            {
                if (null != t.Exception)
                    _log.Error("Error sending email: " + t.Exception.ToString());    
            });
        }
        
    }
}