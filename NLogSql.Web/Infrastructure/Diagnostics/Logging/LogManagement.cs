using NLog;
using NLog.Targets.Wrappers;

namespace NLogSql.Web.Infrastructure.Diagnostics.Logging
{
    public class LogManagement
    {
        public static AsyncTargetWrapper GetAsyncDbWrapperTarget()
        {
            var target = (AsyncTargetWrapper)LogManager.Configuration.FindTargetByName("asyncDbWrapperTarget");
            return target;
        }
    }
}