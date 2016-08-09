namespace NativeCode.Core.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Dependencies.Enums;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Settings;
    using NativeCode.Core.Types;

    /// <summary>
    /// Provides a proxy class so that the <see cref="IApplication" /> interface can be exposed
    /// by types that must be derived, i.e. HttpApplication.
    /// </summary>
    /// <seealso cref="NativeCode.Core.Platform.IApplication" />
    public class ApplicationProxy : DisposableManager, IApplication
    {
        private static int counter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationProxy" /> class.
        /// </summary>
        /// <param name="platform">The platform.</param>
        /// <param name="settings">The settings.</param>
        public ApplicationProxy(IPlatform platform, Settings settings)
        {
            this.Platform = platform;
            this.Settings = settings;

            this.EnsureDisposed(this.Platform);
        }

        public IPlatform Platform { get; private set; }

        public Settings Settings { get; }

        public ICancellationTokenManager CancellationTokens { get; private set; }

        protected bool Initialized { get; private set; }

        public virtual string GetApplicationName()
        {
            return this.GetType().Name.Replace("Application", string.Empty);
        }

        public virtual string GetApplicationVersion()
        {
            return this.GetType().GetTypeInfo().Assembly.GetVersion();
        }

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
                    this.Platform.Registrar.RegisterInstance<IApplication>(this, DependencyLifetime.PerApplication);

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

        protected override void Dispose(bool disposing)
        {
            this.PersistSettings();
            base.Dispose(disposing);
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
            this.CancellationTokens = this.Platform.Resolver.Resolve<ICancellationTokenManager>();
            this.EnsureDisposed(this.CancellationTokens);
        }

        protected virtual void PreInitialization()
        {
        }

        protected virtual void RegisterAssemblies(IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                this.Platform.Registrar.RegisterAssembly(assembly);
            }
        }

        protected virtual void RegisterModule(IDependencyModule module)
        {
            module.RegisterDependencies(this.Platform.Registrar);
        }

        protected virtual void RegisterModules(IEnumerable<IDependencyModule> modules)
        {
            foreach (var module in modules)
            {
                this.RegisterModule(module);
            }
        }

        protected virtual void RestoreSettings()
        {
        }
    }
}