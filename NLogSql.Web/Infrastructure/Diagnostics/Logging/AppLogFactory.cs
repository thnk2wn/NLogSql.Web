using System;
using System.Diagnostics;
using System.Web.Mvc;
using Ninject;
using Ninject.Parameters;

namespace NLogSql.Web.Infrastructure.Diagnostics.Logging
{
    /// <summary>
    /// Creates a log object for those instances where one cannot be injected (i.e. app startup).
    /// Generally you should just ctor inject ILog
    /// </summary>
    public static class AppLogFactory
    {
        public static ILog Create()
        {
            var declaringType = new StackTrace(1, false).GetFrame(1).GetMethod().DeclaringType;

            if (declaringType != null)
            {
                var loggerName = declaringType.FullName;
                return Create(loggerName);
            }

            throw new InvalidOperationException("Could not determine declaring type; specify logger name explicitly");
        }

        public static ILog Create<T>()
        {
            return Create(typeof(T).FullName);
        }

        public static ILog Create(string loggerName)
        {
            var log = Kernel.Get<ILog>(new ConstructorArgument(LogConstants.LoggerNameParam, loggerName));
            return log;
        }

        private static IKernel Kernel
        {
            get
            {
                return DependencyResolver.Current.GetService<IKernel>();
            }
        }
    }

    public class LogConstants
    {
        public const string LoggerNameParam = "loggerName";
    }
}