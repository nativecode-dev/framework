namespace NativeCode.Core.Dependencies
{
    using Enums;

    public abstract class DependencyModule : IDependencyModule
    {
        public DependencyModulePriority Priority { get; protected set; }

        public abstract void RegisterDependencies(IDependencyRegistrar registrar);
    }
}