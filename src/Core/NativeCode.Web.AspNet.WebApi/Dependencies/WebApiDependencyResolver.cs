﻿namespace NativeCode.Web.AspNet.WebApi.Dependencies
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http.Dependencies;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Types;

    using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

    public class WebApiDependencyResolver : Disposable, IDependencyResolver
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
            this.Logger.Debug("Creating new dependency scope.");

            return new WebApiDependencyResolver(this.container.CreateChildContainer(), this.Logger);
        }

        public object GetService(Type serviceType)
        {
            try
            {
                if (this.IgnoreDependency(serviceType) == false)
                {
                    this.Logger.Debug($"Resolving {serviceType.FullName}.");
                    return this.container.Resolver.Resolve(serviceType);
                }
            }
            catch (Exception ex)
            {
                this.Logger.Exception(ex);
            }

            this.Logger.Debug($"Ignoring {serviceType.FullName}.");
            return default(object);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                if (this.IgnoreDependency(serviceType) == false)
                {
                    this.Logger.Debug($"Resolving {serviceType.FullName}.");
                    return this.container.Resolver.ResolveAll(serviceType);
                }
            }
            catch (Exception ex)
            {
                this.Logger.Exception(ex);
            }

            this.Logger.Debug($"Ignoring {serviceType.FullName}.");
            return new object[0];
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.container != null)
            {
                this.container.Dispose();
                this.container = null;
                this.Logger.Debug("Dependency scope disposed.");
            }

            base.Dispose(disposing);
        }

        protected virtual bool IgnoreDependency(Type type)
        {
            if (type != null && string.IsNullOrWhiteSpace(type.Namespace) == false)
            {
                return type.Namespace.StartsWith("System.");
            }

            return false;
        }
    }
}