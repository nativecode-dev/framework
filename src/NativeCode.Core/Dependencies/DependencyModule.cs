namespace NativeCode.Core.Dependencies
{
    public abstract class DependencyModule : IDependencyModule
    {
        public DependencyModulePriority Priority { get; protected set; }

        public abstract void RegisterDependencies(IDependencyRegistrar registrar);
    }
}