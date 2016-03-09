namespace NativeCode.Core.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using NativeCode.Core.Dependencies;

    public class ApplicationCore : IApplication
    {
        public ApplicationCore(IDependencyContainer container, bool owner = true)
        {
            this.Container = container;
            this.ContainerOwner = owner;
        }

        protected bool Disposed { get; private set; }

        protected bool ContainerOwner { get; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IDependencyContainer Container { get; private set; }

        public void Initialize(params IDependencyModule[] modules)
        {
            this.Initialize(Enumerable.Empty<Assembly>(), modules);
        }

        public void Initialize(IEnumerable<Assembly> assemblies, params IDependencyModule[] modules)
        {
            try
            {
                this.PostInitialization();
                this.RegisterAssemblies(assemblies);
                this.RegisterModules(modules);
                this.PostInitialization();
            }
            catch (Exception ex)
            {
                this.FailInitialization(ex);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.Disposed)
            {
                this.Disposed = true;

                if (this.Container != null && this.ContainerOwner)
                {
                    this.Container.Dispose();
                    this.Container = null;
                }
            }
        }

        protected virtual void FailInitialization(Exception ex)
        {
        }

        protected virtual void PostInitialization()
        {
        }

        protected virtual void PreInitialization()
        {
        }

        protected virtual void RegisterAssemblies(IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                this.Container.Registrar.RegisterAssembly(assembly);
            }
        }

        protected virtual void RegisterModules(IEnumerable<IDependencyModule> modules)
        {
            foreach (var module in modules)
            {
                module.RegisterDependencies(this.Container.Registrar);
            }
        }
    }
}