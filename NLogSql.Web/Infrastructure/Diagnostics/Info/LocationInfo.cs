using System.Web.Mvc;
using NLogSql.Web.Infrastructure.Extensions.System_;

namespace NLogSql.Web.Infrastructure.Diagnostics.Info
{
    public class LocationInfo : DiagnosticInfoBase
    {
        private readonly ExceptionContext _exceptionContext;

        public LocationInfo(ExceptionContext exceptionContext)
        {
            _exceptionContext = exceptionContext;
        }

        protected override void GenerateReport()
        {
            var controllerNameFull = _exceptionContext.Controller.GetType().FullName;
            ControllerName = _exceptionContext.Controller.GetType().Name;
            ActionName = _exceptionContext.RouteData.Values["action"].ToString();

            StartTable();
            AppendRow("URL", _exceptionContext.HttpContext.Request.Url);
            AppendRow("Host", ServerInfo.GetMachineName(_exceptionContext.HttpContext));
            AppendRow("Controller", controllerNameFull);
            AppendRow("Action", ActionName);
            EndTable();
        }

        public string ControllerAction
        {
            get { return ".".SmartJoin(ControllerName, ActionName); }
        }

        public string ControllerName { get; private set; }
        public string ActionName { get; private set; }
    }
}