namespace NativeCode.Core.Platform
{
    using System;
    using Dependencies;
    using JetBrains.Annotations;
    using Reliability;
    using Settings;

    /// <summary>
    /// Provides a contract for an application instance.
    /// </summary>
    public interface IApplication : IDisposable
    {
        /// <summary>
        /// Gets the application path.
        /// </summary>
        [NotNull]
        string ApplicationPath { get; }

        /// <summary>
        /// Gets the platform.
        /// </summary>
        [NotNull]
        IPlatform Platform { get; }

        /// <summary>
        /// Gets the root application scope.
        /// </summary>
        [NotNull]
        IApplicationScope Scope { get; }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        [NotNull]
        Settings SettingsObject { get; }

        /// <summary>
        /// Gets the token manager.
        /// </summary>
        [NotNull]
        ICancellationTokenManager CancellationTokens { get; }

        void Configure(params IDependencyModule[] modules);

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
        void Initialize();
    }

    /// <summary>
    /// Provides a contract for an application instance.
    /// </summary>
    public interface IApplication<out TSettings> : IApplication where TSettings : Settings
    {
        /// <summary>
        /// Gets the settings.
        /// </summary>
        [NotNull]
        TSettings Settings { get; }
    }
}