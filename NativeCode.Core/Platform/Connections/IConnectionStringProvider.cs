namespace NativeCode.Core.Platform.Connections
{
    using NativeCode.Core.Types;

    /// <summary>
    /// Provides a contract to return a <see cref="ConnectionString" /> instance.
    /// </summary>
    public interface IConnectionStringProvider
    {
        ConnectionString GetConnectionString(string name);

        ConnectionString GetDefaultConnectionString();
    }
}