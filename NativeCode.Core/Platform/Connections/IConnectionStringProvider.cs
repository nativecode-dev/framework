namespace NativeCode.Core.Platform.Connections
{
    using NativeCode.Core.Types;

    public interface IConnectionStringProvider
    {
        ConnectionString GetDefaultConnectionString();

        ConnectionString GetConnectionString(string name);
    }
}