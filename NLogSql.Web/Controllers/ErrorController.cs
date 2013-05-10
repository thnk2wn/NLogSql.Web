using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using EnsureThat;
using NLog;
using NLogSql.Web.Infrastructure.Diagnostics.Logging;

namespace NLogSql.Web.Controllers
{
    public partial class ErrorController : Controller
    {
        private readonly ILog _log;

        public ErrorController(ILog log)
        {
            _log = Ensure.That(log, "log").IsNotNull().Value;
        }

        public virtual ActionResult NotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            RouteData.Values["fake404"] = true;
            _log.Write(LogType.Warn, new { Code = "404" }, "404 Not Found for {0}", Request.Url);
            return View("Error");
        }

        public virtual ActionResult Gone()
        {
            Response.StatusCode = (int)HttpStatusCode.Gone;
            Response.Status = "410 Gone";
            Response.TrySkipIisCustomErrors = true;
            _log.Write(LogType.Warn, new {Code = "410"}, "410 gone permanently for {0}", Request.Url);
            return View("Error");
        }
    }
}