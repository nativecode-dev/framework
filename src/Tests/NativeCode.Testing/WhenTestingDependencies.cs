namespace NativeCode.Testing
{
    using System.Collections.Generic;
    using Core.Dependencies;
    using Core.Dependencies.Enums;
    using Core.DotNet.Logging;
    using Core.Packages.Unity;
    using Core.Platform.Logging;

    public abstract class WhenTestingDependencies : WhenTesting
    {
        protected WhenTestingDependencies()
        {
            this.Container = new UnityDependencyContainer();
            this.Container.Registrar.Register<ILogger, Logger>();
            this.Container.Registrar.Register<ILogWriter, TraceLogWriter>(DependencyKey.QualifiedName);
            this.Container.Registrar.RegisterFactory(resolver => resolver.ResolveAll<ILogWriter>());
            this.EnsureDisposed(this.Container);
        }

        protected IDependencyContainer Container { get; }

        protected T Resolve<T>()
        {
            return this.Container.Resolver.Resolve<T>();
        }

        protected IEnumerable<T> ResolveAll<T>()
        {
            return this.Container.Resolver.ResolveAll<T>();
        }
    }
}