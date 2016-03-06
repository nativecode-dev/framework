namespace NativeCode.Web.AspNet.Mvc.Dependencies
{
    using System;
    using System.Collections.Generic;

    using NativeCode.Core.Dependencies;

    using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

    public class MvcDependencyResolver : IDependencyResolver
    {
        private readonly IDependencyContainer container;

        public MvcDependencyResolver(IDependencyContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return this.container.Resolver.Resolve(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return this.container.Resolver.ResolveAll(serviceType);
            }
            catch
            {
                return null;
            }
        }
    }
}