using System;
using System.Collections.Generic;
using SimpleInjector;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace Drinks.DI
{
    public class WebApiDependencyInjectorContainer : DependencyInjectorContainerBase
    {
        public static IDependencyResolver GetDependencyResolver(IEnumerable<Type> controllerTypes)
        {
            Container = new Container();
            foreach (var controllerType in controllerTypes)
            {
                Container.Register(controllerType);
            }

            RegisterServices();
            Container.Verify();
            return new SimpleInjectorWebApiDependencyResolver(Container);
        }
    }
}