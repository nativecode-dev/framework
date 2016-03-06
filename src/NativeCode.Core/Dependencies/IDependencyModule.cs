namespace NativeCode.Core.Dependencies
{
    using JetBrains.Annotations;

    public interface IDependencyModule
    {
        DependencyModulePriority Priority { get; }

        void RegisterDependencies([NotNull] IDependencyRegistrar registrar);
    }
}