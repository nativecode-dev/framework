namespace NativeCode.Core.Dependencies
{
    using Enums;
    using JetBrains.Annotations;

    /// <summary>
    /// Provides a contract to encapsulate dependency registrations in
    /// a central location.
    /// </summary>
    public interface IDependencyModule
    {
        /// <summary>
        /// Gets the priority.
        /// </summary>
        /// <value>The priority.</value>
        DependencyModulePriority Priority { get; }

        /// <summary>
        /// Registers the dependencies.
        /// </summary>
        /// <param name="registrar">The registrar.</param>
        void RegisterDependencies([NotNull] IDependencyRegistrar registrar);
    }
}