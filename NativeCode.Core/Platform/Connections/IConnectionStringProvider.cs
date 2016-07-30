namespace NativeCode.Core.Platform.Connections
{
    using NativeCode.Core.Types;

    /// <summary>
    /// Provides a contract to return a <see cref="ConnectionString" /> instance.
    /// </summary>
    public interface IConnectionStringProvider
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Returns the named <see cref="ConnectionString" />.</returns>
        ConnectionString GetConnectionString(string name);

        /// <summary>
        /// Gets the default connection string.
        /// </summary>
        /// <returns>Returns the default <see cref="ConnectionString" />.</returns>
        ConnectionString GetDefaultConnectionString();
    }
}