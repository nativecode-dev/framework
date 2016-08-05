namespace NativeCode.Tests
{
    using System.Collections.Generic;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Dependencies.Enums;
    using NativeCode.Core.DotNet.Logging;
    using NativeCode.Core.Logging;
    using NativeCode.Core.Packages.Unity;

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