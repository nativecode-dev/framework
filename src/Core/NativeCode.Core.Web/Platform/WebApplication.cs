﻿namespace NativeCode.Core.Web.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Compilation;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Settings;
    using NativeCode.Core.Types;

    public abstract class WebApplication : HttpApplication
    {
        /// <summary>
        /// Gets the application proxy.
        /// </summary>
        protected ApplicationProxy ApplicationProxy { get; private set; }

        public override sealed void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Initializes the specified application.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="modules">The modules.</param>
        public virtual void Initialize(string name, params IDependencyModule[] modules)
        {
            this.Initialize(name, BuildManager.GetReferencedAssemblies().Cast<Assembly>(), modules);
        }

        /// <summary>
        /// Initializes the specified application.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="assemblies">The assemblies.</param>
        /// <param name="modules">The modules.</param>
        public virtual void Initialize(string name, IEnumerable<Assembly> assemblies, params IDependencyModule[] modules)
        {
            this.ApplicationProxy = this.CreateApplicationProxy();
            this.ApplicationProxy.Initialize(name, assemblies.Where(this.CanIncludeAssembly), modules);
        }

        /// <summary>
        /// Creates a new application proxy.
        /// </summary>
        /// <returns>Returns a new <see cref="ApplicationProxy" />.</returns>
        protected virtual ApplicationProxy CreateApplicationProxy()
        {
            return new ApplicationProxy(this.CreatePlatform(), this.CreateSettings());
        }

        /// <summary>
        /// Determines whether the assembly should be included.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns><c>true</c> if can include assembly; otherwise, <c>false</c>.</returns>
        protected virtual bool CanIncludeAssembly(Assembly assembly)
        {
            return false;
        }

        /// <summary>
        /// Creates a new platform instance.
        /// </summary>
        /// <returns>Returns a new <see cref="IPlatform" />.</returns>
        protected abstract IPlatform CreatePlatform();

        /// <summary>
        /// Creates application settings.
        /// </summary>
        /// <returns>Returns a new <see cref="Settings" />.</returns>
        protected abstract Settings CreateSettings();

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.ApplicationProxy != null)
                {
                    this.ApplicationProxy.Dispose();
                    this.ApplicationProxy = null;
                }

                base.Dispose();
            }
        }
    }
}