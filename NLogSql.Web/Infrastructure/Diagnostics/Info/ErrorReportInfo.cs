using System;
using System.Linq;
using System.Web.Mvc;

namespace NLogSql.Web.Infrastructure.Diagnostics.Info
{
    public class ErrorReportInfo : DiagnosticInfoBase
    {
        private readonly ExceptionContext _exceptionContext;

        public ErrorReportInfo(ExceptionContext exceptionContext, string customActivityMessage = null)
        {
            _exceptionContext = exceptionContext;
            CustomActivityMessage = customActivityMessage;
        }

        private string CustomActivityMessage { get; set; }

        public LocationInfo Location { get; private set; }
        public ServerInfo Server { get; private set; }

        protected override void GenerateReport()
        {
            AppendHtml("<div style='font-family: Helvetica; font-size: 10pt;'>");

            AppendLine("Time: {0}", DateTime.Now.ToString("G"));
            AppendLine();

            StartSection("Location");
            Location = new LocationInfo(_exceptionContext);
            AppendInfo(Location);
            EndSection();

            StartSection("Error Info");

            if (!string.IsNullOrWhiteSpace(this.CustomActivityMessage))
            {
                AppendLine(this.CustomActivityMessage);
                AppendLine();
            }

            AppendInfo(new ErrorInfo(_exceptionContext.Exception));
            EndSection();

            StartSection("User Info");
            AppendInfo(new UserInfo(_exceptionContext.HttpContext.Request));
            EndSection();

            var cookieInfo = new CookieInfo(_exceptionContext.HttpContext.Request);
            cookieInfo.Generate();
            if (!cookieInfo.IsEmpty)
            {
                StartSection("Request - Cookies");
                AppendInfo(cookieInfo);
                EndSection();
            }

            var formInfo = new FormInfo(_exceptionContext.HttpContext.Request);
            formInfo.Generate();
            if (!formInfo.IsEmpty)
            {
                StartSection("Request - Form");
                AppendInfo(formInfo);
                EndSection();
            }

            var viewDataInfo = new ViewDataInfo(_exceptionContext.Controller);
            viewDataInfo.Generate();
            if (!viewDataInfo.IsEmpty)
            {
                StartSection("View Data");
                AppendInfo(viewDataInfo);
                EndSection();
            }

            StartSection("Request - Server Info");
            Server = new ServerInfo(_exceptionContext.HttpContext.Request);
            AppendInfo(Server);
            EndSection();

            StartSection("Loaded Assemblies");
            AppendInfo(new AssemblyInfo());
            EndSection();

            if (Errors.Any())
            {
                AppendLine("One or more errors occurred generating report:");
                foreach (var error in Errors)
                    AppendLine(error);
            }

            AppendHtml("<div>See also the LogEvent table.</div>");

            AppendHtml("</div>");

            // could inspect loaded assemblies and such
            //AppDomain.CurrentDomain.GetAssemblies()
        }
    }
}