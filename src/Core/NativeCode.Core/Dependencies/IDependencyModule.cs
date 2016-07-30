namespace NativeCode.Core.Dependencies
{
    using JetBrains.Annotations;

    using NativeCode.Core.Dependencies.Enums;

    public interface IDependencyModule
    {
        DependencyModulePriority Priority { get; }

        void RegisterDependencies([NotNull] IDependencyRegistrar registrar);
    }
}