namespace NativeCode.Core.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Security.Principal;
    using Dependencies;
    using JetBrains.Annotations;

    /// <summary>
    /// Provides a contract for communicating with the underlying platform.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IPlatform : IDisposable
    {
        /// <summary>
        /// Gets the application binaries path.
        /// </summary>
        [NotNull]
        string BinariesPath { get; }

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
        /// Gets the current user <see cref="IPrincipal" />.
        /// </summary>
        /// <remarks>
        /// Most platforms will likely use this property to represent the current user of the
        /// process and not the application, hence why it lives on <see cref="IPlatform" />.
        /// </remarks>
        IPrincipal User { get; }

        /// <summary>
        /// Creates a child dependency scope.
        /// </summary>
        /// <returns>Returns a new <see cref="IDependencyContainer" />.</returns>
        IDependencyContainer CreateDependencyScope();

        /// <summary>
        /// Gets the assemblies.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>Returns a collection of <see cref="Assembly" />.</returns>
        IEnumerable<Assembly> GetAssemblies(Func<Assembly, bool> filter);

        /// <summary>
        /// Gets the assemblies.
        /// </summary>
        /// <param name="prefixes">The prefixes.</param>
        /// <returns>Returns a collection of <see cref="Assembly" />.</returns>
        IEnumerable<Assembly> GetAssemblies(params string[] prefixes);
    }
}