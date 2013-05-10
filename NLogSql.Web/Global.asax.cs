using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLogSql.Web.App_Start;
using NLogSql.Web.Controllers;
using NLogSql.Web.Infrastructure.Diagnostics.Logging;

namespace NLogSql.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //var target = LogManagement.GetAsyncDbWrapperTarget();
            //target.OverflowAction = AsyncTargetWrapperOverflowAction.Grow;

            var log = AppLogFactory.Create<MvcApplication>();
            log.Info("Application starting");

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            AutoMapping.CreateMaps();
            
            log.Info("Application started");
        }

        protected void Application_EndRequest()
        {
            if (Context.Response.StatusCode == 404)
            {
                if (Request.RequestContext.RouteData.Values["fake404"] == null)
                {
                    Response.Clear();

                    var rd = new RouteData();
                    rd.Values["controller"] = MVC.Error.Name;
                    rd.Values["action"] = MVC.Error.ActionNames.NotFound;

                    var c = (IController)DependencyResolver.Current.GetService<ErrorController>();
                    Request.RequestContext.RouteData = rd;
                    c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
                }
            }
        }
    }
}