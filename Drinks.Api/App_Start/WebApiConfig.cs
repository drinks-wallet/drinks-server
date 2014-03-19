using System.Web.Http;
using Drinks.Api.ExceptionHandling;
using Drinks.Api.Formatters;

namespace Drinks.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new ExceptionHandlingAttribute());
            config.Formatters.Add(new PlainTextFormatter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
