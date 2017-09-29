namespace NativeCode.Core.Platform
{
    using Dependencies;
    using Dependencies.Attributes;
    using Extensions;
    using Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using Microsoft.Extensions.DependencyInjection;
    using Types;

    /// <summary>
    /// Provides a proxy class so that the <see cref="IApplication{TSettings}" /> interface can be exposed
    /// by types that must be derived, i.e. HttpApplication.
    /// </summary>
    /// <seealso cref="NativeCode.Core.Platform.IApplication{TSetting}" />
    [IgnoreDependency("Use new operator.")]
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

        public virtual string GetApplicationName()
        {
            return this.GetType().Name.Replace("Application", string.Empty);
        }

        public virtual string GetApplicationVersion()
        {
            return this.GetType().GetTypeInfo().Assembly.GetVersion();
        }

        public void Initialize(IDependencyRegistrar registrar, params IDependencyModule[] modules)
        {
            this.Initialize(registrar, Enumerable.Empty<Assembly>(), modules);
        }

        public void Initialize(IDependencyRegistrar registrar, IEnumerable<Assembly> assemblies, params IDependencyModule[] modules)
        {
            if (Interlocked.CompareExchange(ref this.counter, 1, 0) == 0)
            {
                try
                {
                    this.RestoreSettings();
                    this.PreInitialization();
                    this.RegisterDependencies(registrar);
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
                this.EnsureDisposed(this.CancellationTokens);
            }
        }

        protected virtual void PreInitialization()
        {
        }

        protected virtual void RegisterDependencies(IDependencyRegistrar registrar)
        {
            var dependencies = DependencyScanner.Scan(this.Platform.GetAssemblies());

            foreach (var dependency in dependencies)
            {
                registrar.Register(dependency.Contract, dependency.Implementation, dependency.KeyValue, dependency.Lifetime);
            }
        }

        protected virtual void RestoreSettings()
        {
        }
    }
}