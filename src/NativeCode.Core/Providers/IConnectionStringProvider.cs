namespace NativeCode.Core.Providers
{
    using NativeCode.Core.Types;

    public interface IConnectionStringProvider
    {
        ConnectionString GetDefaultConnectionString();

        ConnectionString GetConnectionString(string name);
    }
}