﻿namespace NativeCode.Core.Web.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Compilation;

    using Humanizer;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Settings;

    public abstract class WebApplication : HttpApplication, IWebApplication
    {
        public IPlatform Platform { get; private set; }

        public Settings Settings => this.ApplicationCore?.Settings;

        protected ApplicationCore ApplicationCore { get; private set; }

        public override sealed void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

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
    }
}