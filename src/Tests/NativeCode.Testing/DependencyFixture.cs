namespace NativeCode.Testing
{
    using Core.Dependencies;
    using Core.Dependencies.Enums;
    using Core.DotNet.Logging;
    using Core.Packages.Unity;
    using Core.Platform.Logging;
    using Core.Types;

    public class DependencyFixture : DisposableManager
    {
        public DependencyFixture()
        {
            this.Container = new UnityDependencyContainer();
            this.Container.Registrar.Register<ILogger, Logger>();
            this.Container.Registrar.Register<ILogWriter, TraceLogWriter>(DependencyKey.QualifiedName);
            this.Container.Registrar.RegisterFactory(resolver => resolver.ResolveAll<ILogWriter>());
            this.EnsureDisposed(this.Container);
        }

        protected IDependencyContainer Container { get; }
    }
}