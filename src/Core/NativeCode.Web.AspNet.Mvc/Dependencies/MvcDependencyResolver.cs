namespace NativeCode.Web.AspNet.Mvc.Dependencies
{
    using System;
    using System.Collections.Generic;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Logging;

    using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

    public class MvcDependencyResolver : IDependencyResolver
    {
        private readonly IDependencyContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="MvcDependencyResolver" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="logger">The logger.</param>
        public MvcDependencyResolver(IDependencyContainer container, ILogger logger)
        {
            this.container = container;
            this.Logger = logger;
        }

        protected ILogger Logger { get; }

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
    }
}