namespace NativeCode.Web.Platform
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

    public abstract class WebApplication : HttpApplication, IWebApplication
    {
        protected ApplicationCore ApplicationCore { get; private set; }

        public override void Dispose()
        {
            this.Dispose(true);
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public IDependencyContainer Container => this.ApplicationCore?.Container;

        public Settings Settings => this.ApplicationCore?.Settings;

        public virtual void Initialize(string name, params IDependencyModule[] modules)
        {
            this.Initialize(name, BuildManager.GetReferencedAssemblies().Cast<Assembly>(), modules);
        }

        public virtual void Initialize(string name, IEnumerable<Assembly> assemblies, params IDependencyModule[] modules)
        {
            this.ApplicationCore = new ApplicationCore(this.CreateDependencyContainer());
            this.ApplicationCore.Initialize(name, assemblies.Where(this.CanIncludeAssembly), modules);
        }

        protected virtual bool CanIncludeAssembly(Assembly assembly)
        {
            return false;
        }

        protected abstract IDependencyContainer CreateDependencyContainer();
    }
}