namespace NativeCode.Core.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Security.Principal;

    using JetBrains.Annotations;

    using NativeCode.Core.Dependencies;

    /// <summary>
    /// Provides a contract for communicating with the underlying platform.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IPlatform : IDisposable
    {
        /// <summary>
        /// Gets the application path.
        /// </summary>
        [NotNull]
        string ApplicationPath { get; }

        /// <summary>
        /// Gets the data path.
        /// </summary>
        [NotNull]
        string DataPath { get; }

        /// <summary>
        /// Gets the name of the machine.
        /// </summary>
        [NotNull]
        string MachineName { get; }

        /// <summary>
        /// Gets the registrar.
        /// </summary>
        IDependencyRegistrar Registrar { get; }

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        IDependencyResolver Resolver { get; }

        /// <summary>
        /// Creates a child dependency scope.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>Returns a new <see cref="IDependencyContainer" />.</returns>
        IDependencyContainer CreateDependencyScope(IDependencyContainer parent = default(IDependencyContainer));

        /// <summary>
        /// Gets the assemblies.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>Returns a collection of <see cref="Assembly" />.</returns>
        IEnumerable<Assembly> GetAssemblies(Func<Assembly, bool> filter = null);

        /// <summary>
        /// Gets the assemblies.
        /// </summary>
        /// <param name="prefixes">The prefixes.</param>
        /// <returns>Returns a collection of <see cref="Assembly" />.</returns>
        IEnumerable<Assembly> GetAssemblies(params string[] prefixes);

        /// <summary>
        /// Gets the current principal.
        /// </summary>
        /// <returns>Returns a <see cref="IPrincipal" />.</returns>
        IPrincipal GetCurrentPrincipal();
    }
}