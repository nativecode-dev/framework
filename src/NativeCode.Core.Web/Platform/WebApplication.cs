﻿namespace NativeCode.Core.Web.Platform
{
    using Humanizer;
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Compilation;

    public abstract class WebApplication : HttpApplication, IWebApplication
    {
        protected ApplicationCore ApplicationCore { get; private set; }

        public sealed override void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Platform != null)
                {
                    this.Platform.Dispose();
                    this.Platform = null;
                }

                base.Dispose();
            }
        }

        public IPlatform Platform { get; private set; }

        public Settings Settings => this.ApplicationCore?.Settings;

        public virtual string GetApplicationName()
        {
            return this.GetType().Name.Replace("Application", string.Empty).Humanize(LetterCasing.Title);
        }

        public virtual string GetApplicationVersion()
        {
            return null;
        }

        public virtual void Initialize(string name, params IDependencyModule[] modules)
        {
            this.Initialize(name, BuildManager.GetReferencedAssemblies().Cast<Assembly>(), modules);
        }

        public virtual void Initialize(string name, IEnumerable<Assembly> assemblies, params IDependencyModule[] modules)
        {
            this.ApplicationCore = new ApplicationCore(this.CreatePlatform());
            this.ApplicationCore.Initialize(name, assemblies.Where(this.CanIncludeAssembly), modules);
        }

        protected virtual bool CanIncludeAssembly(Assembly assembly)
        {
            return false;
        }

        protected abstract IPlatform CreatePlatform();
    }
}
