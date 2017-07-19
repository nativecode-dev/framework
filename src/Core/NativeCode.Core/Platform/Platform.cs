namespace NativeCode.Core.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Security.Principal;
    using Dependencies;
    using Dependencies.Enums;
    using Types;

    /// <summary>
    /// Abstract class to manage the underlying platform.
    /// </summary>
    /// <seealso cref="NativeCode.Core.Types.Disposable" />
    /// <seealso cref="NativeCode.Core.Platform.IPlatform" />
    public abstract class Platform : Disposable, IPlatform
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Platform" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        protected Platform(IDependencyContainer container)
        {
            this.Container = container;
            this.Registrar.RegisterInstance<IPlatform>(this, DependencyLifetime.PerApplication);
        }

        public abstract string BinariesPath { get; }

        public abstract string DataPath { get; }

        public abstract string MachineName { get; }

        public IDependencyRegistrar Registrar => this.Container.Registrar;

        public IDependencyResolver Resolver => this.Container.Resolver;

        protected IDependencyContainer Container { get; private set; }

        public virtual IDependencyContainer CreateDependencyScope(IDependencyContainer parent = null)
        {
            if (parent != null)
                return parent.CreateChildContainer();

            return this.Container.CreateChildContainer();
        }

        public abstract IEnumerable<Assembly> GetAssemblies(Func<Assembly, bool> filter = null);

        public abstract IEnumerable<Assembly> GetAssemblies(params string[] prefixes);

        public abstract IPrincipal GetCurrentPrincipal();

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.Disposed)
                if (this.Container != null)
                {
                    this.Container.Dispose();
                    this.Container = null;
                }

            base.Dispose(disposing);
        }
    }
}