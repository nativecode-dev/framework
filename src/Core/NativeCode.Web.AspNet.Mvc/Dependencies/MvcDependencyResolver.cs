namespace NativeCode.Web.AspNet.Mvc.Dependencies
{
    using System;
    using System.Collections.Generic;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Types;

    using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

    public class MvcDependencyResolver : Disposable, IDependencyResolver
    {
        private IDependencyContainer container;

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
                if (IsFiltered(serviceType) == false)
                {
                    return this.container.Resolver.Resolve(serviceType);
                }
            }
            catch (Exception ex)
            {
                this.Logger.Exception(ex);
            }

            return default(object);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                if (IsFiltered(serviceType) == false)
                {
                    return this.container.Resolver.ResolveAll(serviceType);
                }
            }
            catch (Exception ex)
            {
                this.Logger.Exception(ex);
            }

            return default(IEnumerable<object>);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.container != null)
            {
                this.container.Dispose();
                this.container = null;
            }
        }

        private static bool IsFiltered(Type type)
        {
            if (type != null && string.IsNullOrWhiteSpace(type.Namespace) == false)
            {
                return type.Namespace.StartsWith("System.");
            }

            return false;
        }
    }
}