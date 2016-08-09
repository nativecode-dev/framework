namespace NativeCode.Core.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Settings;
    using NativeCode.Core.Types;

    /// <summary>
    /// Provides a contract for an application instance.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IApplication : IDisposable
    {
        /// <summary>
        /// Gets the platform.
        /// </summary>
        IPlatform Platform { get; }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        Settings Settings { get; }

        /// <summary>
        /// Gets the token manager.
        /// </summary>
        ICancellationTokenManager CancellationTokens { get; }

        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <returns>Returns the application title.</returns>
        string GetApplicationName();

        /// <summary>
        /// Gets the application version.
        /// </summary>
        /// <returns>Returns the application version.</returns>
        string GetApplicationVersion();

        /// <summary>
        /// Initializes the specified application.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="modules">The modules.</param>
        void Initialize(string name, params IDependencyModule[] modules);

        /// <summary>
        /// Initializes the specified application.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="assemblies">The assemblies.</param>
        /// <param name="modules">The modules.</param>
        void Initialize(string name, IEnumerable<Assembly> assemblies, params IDependencyModule[] modules);
    }
}