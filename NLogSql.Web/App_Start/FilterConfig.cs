using System.Web.Mvc;
using NLogSql.Web.Filters;
using NLogSql.Web.Infrastructure.ErrorHandling;

namespace NLogSql.Web.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AppErrorHandlerAttribute());
            filters.Add(new ActionTrackerAttribute());
        }
    }
}