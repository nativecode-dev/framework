namespace NativeCode.Core.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using Dependencies;
    using Dependencies.Attributes;
    using Dependencies.Enums;
    using Extensions;
    using Settings;
    using Types;

    /// <summary>
    /// Provides a proxy class so that the <see cref="IApplication{TSettings}" /> interface can be exposed
    /// by types that must be derived, i.e. HttpApplication.
    /// </summary>
    /// <seealso cref="NativeCode.Core.Platform.IApplication{TSetting}" />
    [IgnoreDependency("Ue new operator.")]
    public abstract class Application<TSettings> : DisposableManager, IApplication<TSettings> where TSettings : Settings
    {
        /// <summary>
        /// Counter for keeping track of registration of this instance with DI.
        /// </summary>
        private int counter;

        /// <summary>
        /// Initializes a new instance of the <see cref="Application{TSettings}" /> class.
        /// </summary>
        /// <param name="platform">The platform.</param>
        /// <param name="settings">The settings.</param>
        protected Application(IPlatform platform, TSettings settings)
        {
            this.ApplicationPath = platform.DataPath;
            this.Platform = platform;
            this.Scope = new ApplicationScope(this.Platform.CreateDependencyScope());
            this.Settings = settings;

            this.EnsureDisposed(this.Platform);
        }

        public string ApplicationPath { get; protected set; }

        public IPlatform Platform { get; }

        public IApplicationScope Scope { get; }

        public TSettings Settings { get; }

        public Settings SettingsObject => this.Settings;

        public ICancellationTokenManager CancellationTokens { get; private set; }

        protected bool Initialized { get; private set; }

        protected IDependencyRegistrar Registrar => this.Platform.Registrar;

        protected IDependencyResolver Resolver => this.Platform.Resolver;

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
            if (Interlocked.CompareExchange(ref this.counter, 1, 0) == 0)
                try
                {
                    this.Registrar.RegisterInstance<IApplication>(this, DependencyLifetime.PerApplication);
                    this.Registrar.RegisterInstance<IApplication<TSettings>>(this, DependencyLifetime.PerApplication);

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

        protected override void Dispose(bool disposing)
        {
            this.PersistSettings();
            base.Dispose(disposing);
        }

        protected void EnsureInitialized()
        {
            if (this.Initialized == false)
                throw new InvalidOperationException("Application was not initialized.");
        }

        protected virtual void FailInitialization(Exception ex)
        {
        }

        protected virtual void PersistSettings()
        {
        }

        protected virtual void PostInitialization()
        {
            this.CancellationTokens = this.Resolver.Resolve<ICancellationTokenManager>();
            this.EnsureDisposed(this.CancellationTokens);
        }

        protected virtual void PreInitialization()
        {
        }

        protected virtual void RegisterAssemblies(IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
                this.Registrar.RegisterAssembly(assembly);
        }

        protected virtual void RegisterModule(IDependencyModule module)
        {
            module.RegisterDependencies(this.Registrar);
        }

        protected virtual void RegisterModules(IEnumerable<IDependencyModule> modules)
        {
            foreach (var module in modules)
                this.RegisterModule(module);
        }

        protected virtual void RestoreSettings()
        {
        }
    }
}