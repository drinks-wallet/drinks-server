using System.Web.Http;

namespace Drinks.Api
{
    using System.Web.Http.ExceptionHandling;
    using Drinks.Api.ExceptionHandling;
    using Drinks.Api.Formatters;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Services.Add(typeof(IExceptionLogger), new DefaultExceptionLogger());
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
