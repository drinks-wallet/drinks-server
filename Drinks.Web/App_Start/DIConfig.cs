using System.Reflection;
using System.Web.Mvc;
using Drinks.DI;

namespace Drinks.Web.App_Start
{
    public static class DIConfig
    {
        public static void Initialize()
        {
            DependencyResolver.SetResolver(MvcDependencyInjectorContainer.GetDependencyResolver(Assembly.GetExecutingAssembly()));
        }
    }
}