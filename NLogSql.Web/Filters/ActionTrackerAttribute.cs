using System;
using System.Diagnostics;
using System.Web.Mvc;
using NLogSql.Web.Infrastructure.Diagnostics.Logging;

namespace NLogSql.Web.Filters
{
    public class ActionTrackerAttribute : ActionFilterAttribute
    {
        private Stopwatch Watch { get; set; }

        private ILog Log { get; set; }
        private ActionExecutingContext FilterContext { get; set; }

        private string ActionName
        {
            get
            {
                return FilterContext.ActionDescriptor.ActionName;
            }
        }

        private string ControllerName
        {
            get
            {
                return FilterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            }
        }

        private Uri Url
        {
            get { return FilterContext.RequestContext.HttpContext.Request.Url; }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            try
            {
                FilterContext = filterContext;
                Log = AppLogFactory.Create<ActionTrackerAttribute>();
                Log.Trace("Executing {0}.{1}", ControllerName, ActionName);
                Watch = Stopwatch.StartNew();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);

            try
            {
                Watch.Stop();
                Log.Info("Executed {0}.{1} for {2} in {3:##0.000} second(s)", ControllerName, ActionName, Url,
                         Watch.Elapsed.TotalSeconds);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }
    }
}