using System;
using System.Collections.Generic;
using Drinks.DI.Annotations;
using SimpleInjector;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace Drinks.DI
{
    [UsedImplicitly]
    public class WebApiDependencyInjectorContainer : DependencyInjectorContainerBase
    {
        public static IDependencyResolver GetDependencyResolver(IEnumerable<Type> controllerTypes)
        {
            foreach (var controllerType in controllerTypes)
            {
                Container.Register(controllerType);
            }

            Container.Verify();
            return new SimpleInjectorWebApiDependencyResolver(Container);
        }
    }
}