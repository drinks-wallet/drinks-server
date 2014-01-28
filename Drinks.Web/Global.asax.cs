using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Drinks.Web.App_Start;

namespace Drinks.Web
{
    using System.Web.Optimization;
    using Drinks.Web.CustomModelBinders;

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
