namespace NativeCode.Core.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Dependencies.Enums;
    using NativeCode.Core.Types;

    public abstract class Platform : Disposable, IPlatform
    {
        protected Platform(IDependencyContainer container)
        {
            this.Container = container;
            this.Registrar.RegisterInstance<IPlatform>(this, DependencyLifetime.PerApplication);
        }

        public abstract string ApplicationPath { get; }

        public abstract string DataPath { get; }

        public abstract string MachineName { get; }

        public IDependencyRegistrar Registrar => this.Container.Registrar;

        public IDependencyResolver Resolver => this.Container.Resolver;

        protected IDependencyContainer Container { get; private set; }

        public abstract Task<IPrincipal> AuthenticateAsync(string login, string password, CancellationToken cancellationToken);

        public virtual IDependencyContainer CreateDependencyScope(IDependencyContainer parent = null)
        {
            if (parent != null)
            {
                return parent.CreateChildContainer();
            }

            return this.Container.CreateChildContainer();
        }

        public abstract IEnumerable<Assembly> GetAssemblies(Func<Assembly, bool> filter = null);

        public abstract IEnumerable<Assembly> GetAssemblies(params string[] prefixes);

        public abstract IPrincipal GetCurrentPrincipal();

        public abstract IEnumerable<string> GetCurrentRoles();

        public abstract void SetCurrentPrincipal(IPrincipal principal);

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.Disposed)
            {
                if (this.Container != null)
                {
                    this.Container.Dispose();
                    this.Container = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}