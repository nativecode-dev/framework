namespace NativeCode.Core.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Settings;

    public class ApplicationCore : IApplication
    {
        private static int counter;

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

        public Settings Settings { get; } = new JsonSettings();

        protected bool Initialized { get; private set; }

        public void Initialize(string name, params IDependencyModule[] modules)
        {
            this.Initialize(name, Enumerable.Empty<Assembly>(), modules);
        }

        public void Initialize(string name, IEnumerable<Assembly> assemblies, params IDependencyModule[] modules)
        {
            if (Interlocked.CompareExchange(ref counter, 1, 0) == 0)
            {
                try
                {
                    this.Container.Registrar.RegisterInstance<IApplication>(this);

                    this.RestoreSettings();
                    this.PreInitialization();
                    this.RegisterAssemblies(assemblies);
                    this.RegisterModules(modules);
                    this.PostInitialization();

                    this.Initialized = true;
                }
                catch (Exception ex)
                {
                    this.Initialized = false;
                    this.FailInitialization(ex);
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.Disposed)
            {
                this.Disposed = true;

                this.PersistSettings();

                if (this.Container != null && this.ContainerOwner)
                {
                    this.Container.Dispose();
                    this.Container = null;
                }
            }
        }

        protected void EnsureInitialized()
        {
            if (!this.Initialized)
            {
                throw new InvalidOperationException("Application was not initialized.");
            }
        }

        protected virtual void FailInitialization(Exception ex)
        {
        }

        protected virtual void PersistSettings()
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

        protected virtual void RestoreSettings()
        {
        }
    }
}