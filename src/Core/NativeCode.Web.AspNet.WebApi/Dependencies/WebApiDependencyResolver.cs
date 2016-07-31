namespace NativeCode.Web.AspNet.WebApi.Dependencies
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http.Dependencies;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Logging;

    using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

    public class WebApiDependencyResolver : IDependencyResolver
    {
        private IDependencyContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebApiDependencyResolver" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="logger">The logger.</param>
        public WebApiDependencyResolver(IDependencyContainer container, ILogger logger)
        {
            this.container = container;
            this.Logger = logger;
        }

        protected ILogger Logger { get; }

        public IDependencyScope BeginScope()
        {
            return new WebApiDependencyResolver(this.container.CreateChildContainer(), this.Logger);
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
            catch (Exception ex)
            {
                this.Logger.Exception(ex);
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return this.container.Resolver.ResolveAll(serviceType);
            }
            catch (Exception ex)
            {
                this.Logger.Exception(ex);
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