using System.Reflection;
using Drinks.DI.Annotations;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace Drinks.DI
{
    [UsedImplicitly]
    public class MvcDependencyInjectorContainer : DependencyInjectorContainerBase
    {
        public static IDependencyResolver GetDependencyResolver(Assembly controllersAssembly)
        {
            Container.RegisterMvcControllers(controllersAssembly);
            Container.RegisterMvcAttributeFilterProvider();
            RegisterServices();
            return new SimpleInjectorDependencyResolver(Container);
        }
    }
}
