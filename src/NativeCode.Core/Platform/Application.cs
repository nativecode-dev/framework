namespace NativeCode.Core.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;
    using Dependencies;
    using Extensions;
    using Reliability;
    using Settings;

    /// <summary>
    /// Provides a proxy class so that the <see cref="IApplication{TSettings}" /> interface can be exposed
    /// by types that must be derived, i.e. HttpApplication.
    /// </summary>
    /// <seealso cref="NativeCode.Core.Platform.IApplication{TSetting}" />
    public abstract class Application<TSettings> : DisposableManager, IApplication<TSettings> where TSettings : Settings
    {
        private readonly IDependencyContainer container;

        /// <summary>
        /// Counter for keeping track of registration of this instance with DI.
        /// </summary>
        private int counter;

        /// <summary>
        /// Initializes a new instance of the <see cref="Application{TSettings}" /> class.
        /// </summary>
        /// <param name="platform">The platform.</param>
        /// <param name="container">The container.</param>
        protected Application(IPlatform platform, IDependencyContainer container)
        {
            this.container = container;

            this.ApplicationPath = platform.DataPath;
            this.Platform = platform;
            this.Scope = new ApplicationScope(container);

            this.DeferDispose(this.Platform);
        }

        public string ApplicationPath { get; protected set; }

        public IPlatform Platform { get; }

        public IApplicationScope Scope { get; }

        public TSettings Settings { get; private set; }

        public Settings SettingsObject => this.Settings;

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

        public void Configure(params IDependencyModule[] modules)
        {
            this.RegisterAssemblies();
            this.RegisterModules(modules);
        }

        public void Initialize()
        {
            if (Interlocked.CompareExchange(ref this.counter, 1, 0) == 0)
            {
                try
                {
                    this.PreInitialization();
                    this.RestoreSettings();
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
            if (this.Initialized == false)
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
            using (var resolver = this.Scope.CreateResolver())
            {
                this.CancellationTokens = resolver.Resolve<ICancellationTokenManager>();
                this.DeferDispose(this.CancellationTokens);
            }
        }

        protected virtual void PreInitialization()
        {
        }

        protected virtual void RegisterAssemblies()
        {
            var dependencies = DependencyScanner.Scan(this.Platform.GetAssemblies());

            foreach (var dependency in dependencies)
            {
                this.container.Registrar.Register(dependency.Contract, dependency.Implementation, dependency.KeyValue, dependency.Lifetime);
            }
        }

        protected virtual void RegisterModules(IEnumerable<IDependencyModule> modules)
        {
            foreach (var module in modules)
            {
                module.RegisterDependencies(this.container.Registrar);
            }
        }

        protected virtual void RestoreSettings()
        {
            using (var resolver = this.Scope.CreateResolver())
            {
                this.Settings = resolver.Resolve<TSettings>();
            }
        }
    }
}