using System;
using System.Linq;
using System.Web.Mvc;
using NLog;
using NLogSql.Web.Infrastructure.Diagnostics.Logging;
using NLogSql.Web.Infrastructure.ErrorHandling;
using Ninject.Activation;
using Ninject.Modules;
using Ninject.Web.Mvc.FilterBindingSyntax;

namespace NLogSql.Web.DI
{
    public class DiagnosticsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILog>().ToMethod(CreateLog);
            Bind<IErrorReporter>().To<ErrorReporter>();
            Bind<IEventLogWriter>().To<EventLogWriter>();
            Bind<IErrorEmailer>().To<ErrorEmailer>();

            Kernel.BindFilter<AppErrorHandlerAttribute>(FilterScope.Controller, 0);
        }

        private static ILog CreateLog(IContext ctx)
        {
            var p = ctx.Parameters.FirstOrDefault(x => x.Name == LogConstants.LoggerNameParam);
            var loggerName = (null != p) ? p.GetValue(ctx, null).ToString() : null;

            if (string.IsNullOrWhiteSpace(loggerName))
            {
                if (null == ctx.Request.ParentRequest)
                {
                    throw new NullReferenceException(
                        "ParentRequest is null; unable to determine logger name; if not injecting into a ctor "
                        + "a parameter for the logger name must be provided");
                }

                var service = ctx.Request.ParentRequest.Service;
                loggerName = service.FullName;
            }

            var log = (ILog)LogManager.GetLogger(loggerName, typeof(Log));
            return log;
        }
    }
}