namespace NativeCode.Core.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Security.Principal;
    using Dependencies;
    using Reliability;
    using Security;

    /// <summary>
    /// Abstract class to manage the underlying platform.
    /// </summary>
    /// <seealso cref="Disposable" />
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
            this.User = ApplicationPrincipal.System;

            DependencyLocator.SetRootContainer(container);
        }

        public virtual string BinariesPath => AppDomain.CurrentDomain.BaseDirectory;

        public virtual string DataPath => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public virtual string MachineName => Environment.MachineName;

        public IPrincipal User { get; protected set; }

        protected IDependencyContainer Container { get; private set; }

        public virtual IDependencyContainer CreateDependencyScope()
        {
            return this.Container.CreateChildContainer();
        }

        public virtual IEnumerable<Assembly> GetAssemblies(Func<Assembly, bool> filter)
        {
            var assemblies = this.GetAssemblies().ToList();

            if (filter != null)
            {
                return assemblies.Where(filter);
            }

            return assemblies;
        }

        public virtual IEnumerable<Assembly> GetAssemblies(params string[] prefixes)
        {
            return this.GetAssemblies(assembly => prefixes.Any(prefix => assembly.FullName.StartsWith(prefix)));
        }

        protected override void ReleaseManaged()
        {
            this.Container.Dispose();
        }

        protected abstract IEnumerable<Assembly> GetAssemblies();
    }
}