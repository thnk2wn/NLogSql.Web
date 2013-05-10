using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnsureThat;
using NLogSql.Web.Infrastructure.Diagnostics.Logging;
using Ninject;

namespace NLogSql.Web.Infrastructure.ErrorHandling
{
    public class AppErrorHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        [Inject]
        public IErrorReporter Reporter { get; set; }

        public void OnException(ExceptionContext exceptionContext)
        {
            if (exceptionContext.ExceptionHandled) return;

            if (ConfigurationManager.AppSettings["enableErrorPages"] == "false")
            {
                AppLogFactory.Create<AppErrorHandlerAttribute>().Error(
                    "Unexpected error. enableErrorPages is false, skipping detailed error gathering. Error was: {0}",
                    exceptionContext.Exception.ToString());
                return;
            }

            Ensure.That(Reporter, "Reporter").IsNotNull();
            Reporter.ReportException(exceptionContext);

            SetErrorViewResult(exceptionContext);
        }

        private static void SetErrorViewResult(ExceptionContext exceptionContext)
        {
            var statusCode = new HttpException(null, exceptionContext.Exception).GetHttpCode();

            exceptionContext.Result = new ViewResult
            {
                ViewName = MVC.Shared.Views.ViewNames.Error,
                TempData = exceptionContext.Controller.TempData,
                //ViewData = new ViewDataDictionary<ErrorModel>(new ErrorModel())
            };

            exceptionContext.ExceptionHandled = true;
            exceptionContext.HttpContext.Response.Clear();
            exceptionContext.HttpContext.Response.StatusCode = statusCode;
            exceptionContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}