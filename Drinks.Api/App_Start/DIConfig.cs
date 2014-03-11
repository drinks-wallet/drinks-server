using Drinks.DI;

namespace Drinks.Web.App_Start
{
    using System.Web.Http;

    public static class DIConfig
    {
        public static void Initialize()
        {
            var services = GlobalConfiguration.Configuration.Services;
            var controllerTypes = services.GetHttpControllerTypeResolver().GetControllerTypes(services.GetAssembliesResolver());
            GlobalConfiguration.Configuration.DependencyResolver = WebApiDependencyInjectorContainer.GetDependencyResolver(controllerTypes);
        }
    }
}