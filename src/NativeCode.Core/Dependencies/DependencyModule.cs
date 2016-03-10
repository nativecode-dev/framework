namespace NativeCode.Core.Dependencies
{
    using NativeCode.Core.Dependencies.Enums;

    public abstract class DependencyModule : IDependencyModule
    {
        public DependencyLifetime PerApplication => DependencyLifetime.PerApplication;

        public DependencyLifetime PerContainer => DependencyLifetime.PerContainer;

        public DependencyLifetime PerResolve => DependencyLifetime.PerResolve;

        public DependencyModulePriority Priority { get; protected set; }

        public abstract void RegisterDependencies(IDependencyRegistrar registrar);
    }
}