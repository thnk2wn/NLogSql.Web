using System;
using System.Web.Mvc;

namespace NLogSql.Web.Infrastructure.ErrorHandling
{
    public interface IErrorReporter
    {
        /// <summary>
        /// Report an exception that isn't unhandled (caught).
        /// </summary>
        /// <param name="controllerContext">this.ControllerContext from within a controller</param>
        /// <param name="exception">exception that occurred</param>
        /// <param name="customActivityMessage">Any custom activity message to prepend to error report to give context</param>
        void ReportException(ControllerContext controllerContext, Exception exception, string customActivityMessage = null);

        void ReportException(ExceptionContext exceptionContext);
    }
}
