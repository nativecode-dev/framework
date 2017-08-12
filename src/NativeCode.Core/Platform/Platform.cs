namespace NativeCode.Core.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Security.Principal;
    using Dependencies;
    using Dependencies.Enums;
    using Security;
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
            this.User = ApplicationPrincipal.System;
        }

        public virtual string BinariesPath { get; } = string.Empty;

        public virtual string DataPath { get; } = string.Empty;

        public virtual string MachineName { get; } = Environment.MachineName;

        public IDependencyRegistrar Registrar => this.Container.Registrar;

        public IDependencyResolver Resolver => this.Container.Resolver;

        public IPrincipal User { get; protected set; }

        protected IDependencyContainer Container { get; private set; }

        public virtual IDependencyContainer CreateDependencyScope(IDependencyContainer parent = null)
        {
            if (parent != null)
                return parent.CreateChildContainer();

            return this.Container.CreateChildContainer();
        }

        public virtual IEnumerable<Assembly> GetAssemblies(Func<Assembly, bool> filter)
        {
            var assemblies = this.GetAssemblies().ToList();

            if (filter != null)
                return assemblies.Where(filter);

            return assemblies;
        }

        public virtual IEnumerable<Assembly> GetAssemblies(params string[] prefixes)
        {
            return this.GetAssemblies(assembly => prefixes.Any(prefix => assembly.FullName.StartsWith(prefix)));
        }

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

        protected abstract IEnumerable<Assembly> GetAssemblies();
    }
}