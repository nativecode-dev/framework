namespace NativeCode.Core.Platform.Connections
{
    using JetBrains.Annotations;

    /// <summary>
    /// Provides a contract to return a <see cref="ConnectionString" /> instance.
    /// </summary>
    public interface IConnectionStringProvider
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <typeparam name="T">The type to name.</typeparam>
        /// <returns>Returns the named <see cref="ConnectionString" />.</returns>
        [NotNull]
        ConnectionString GetConnectionString<T>();

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Returns the named <see cref="ConnectionString" />.</returns>
        [NotNull]
        ConnectionString GetConnectionString([NotNull] string name);

        /// <summary>
        /// Gets the default connection string.
        /// </summary>
        /// <returns>Returns the default <see cref="ConnectionString" />.</returns>
        [NotNull]
        ConnectionString GetDefaultConnectionString();
    }
}