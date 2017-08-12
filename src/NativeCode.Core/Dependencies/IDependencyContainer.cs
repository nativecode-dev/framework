namespace NativeCode.Core.Dependencies
{
    using System;

    /// <summary>
    /// Provides a contract for registering and resolving dependencies.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IDependencyContainer : IDisposable
    {
        /// <summary>
        /// Gets the registrar.
        /// </summary>
        /// <value>The registrar.</value>
        IDependencyRegistrar Registrar { get; }

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <value>The resolver.</value>
        IDependencyResolver Resolver { get; }

        /// <summary>
        /// Creates the child container.
        /// </summary>
        /// <returns>IDependencyContainer.</returns>
        IDependencyContainer CreateChildContainer();
    }
}