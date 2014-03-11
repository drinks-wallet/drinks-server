using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http.Dependencies;
using SimpleInjector;

namespace Drinks.DI
{
    public sealed class SimpleInjectorWebApiDependencyResolver : IDependencyResolver
    {
        readonly Container _container;

        public SimpleInjectorWebApiDependencyResolver(
            Container container)
        {
            _container = container;
        }

        [DebuggerStepThrough]
        public IDependencyScope BeginScope()
        {
            return this;
        }

        [DebuggerStepThrough]
        public object GetService(Type serviceType)
        {
            return ((IServiceProvider)_container)
                .GetService(serviceType);
        }

        [DebuggerStepThrough]
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
        }

        [DebuggerStepThrough]
        public void Dispose()
        {
        }
    }
}