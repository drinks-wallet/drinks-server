using System.Reflection;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace Drinks.DI
{
    public class MvcDependencyInjectorContainer : DependencyInjectorContainerBase
    {
        public static IDependencyResolver GetDependencyResolver(Assembly controllersAssembly)
        {
            Container = new Container();
            Container.RegisterMvcControllers(controllersAssembly);
            Container.RegisterMvcAttributeFilterProvider();
            RegisterServices();
            return new SimpleInjectorDependencyResolver(Container);
        }
    }
}
