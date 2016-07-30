namespace NativeCode.Web.AspNet.WebApi.Dependencies
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http.Dependencies;

    using NativeCode.Core.Dependencies;

    using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

    public class WebApiDependencyResolver : IDependencyResolver
    {
        private IDependencyContainer container;

        public WebApiDependencyResolver(IDependencyContainer container)
        {
            this.container = container;
        }

        public IDependencyScope BeginScope()
        {
            return new WebApiDependencyResolver(this.container.CreateChildContainer());
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
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

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this.container != null)
            {
                this.container.Dispose();
                this.container = null;
            }
        }
    }
}