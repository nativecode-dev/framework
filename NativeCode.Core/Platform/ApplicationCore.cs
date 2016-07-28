﻿namespace NativeCode.Core.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;

    using Humanizer;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Dependencies.Enums;
    using NativeCode.Core.Settings;

    public class ApplicationCore : IApplication
    {
        private static int counter;

        public ApplicationCore(IPlatform platform)
        {
            this.Platform = platform;
        }

        protected bool Disposed { get; private set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IPlatform Platform { get; private set; }

        public Settings Settings { get; } = new JsonSettings();

        public virtual string GetApplicationName()
        {
            return this.GetType().Name.Replace("Application", string.Empty).Humanize(LetterCasing.Title);
        }

        public virtual string GetApplicationVersion()
        {
            return null;
        }

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

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.Disposed)
            {
                this.Disposed = true;

                this.PersistSettings();

                if (this.Platform != null)
                {
                    this.Platform.Dispose();
                    this.Platform = null;
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