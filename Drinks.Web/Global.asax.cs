using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Drinks.Web.App_Start;
using System.Web.Optimization;

namespace Drinks.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            DIConfig.Initialize();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinderConfig.RegisterModelBinders();
        }
    }
}
