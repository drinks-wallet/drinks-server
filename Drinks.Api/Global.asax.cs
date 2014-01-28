using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Drinks.Web.App_Start;

namespace Drinks.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            DIConfig.Initialize();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
