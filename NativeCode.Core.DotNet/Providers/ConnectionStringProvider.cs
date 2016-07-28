namespace NativeCode.Core.DotNet.Providers
{
    using System;
    using System.Configuration;

    using NativeCode.Core.Platform.Connections;
    using NativeCode.Core.Types;

    public class ConnectionStringProvider : IConnectionStringProvider
    {
        public virtual ConnectionString GetDefaultConnectionString()
        {
            return this.GetConnectionString("Default");
        }

        public virtual ConnectionString GetConnectionString(string name)
        {
            foreach (ConnectionStringSettings connectionString in ConfigurationManager.ConnectionStrings)
            {
                if (string.Equals(connectionString.Name, name, StringComparison.CurrentCultureIgnoreCase))
                {
                    return new ConnectionString(connectionString.ConnectionString);
                }
            }

            return default(ConnectionString);
        }
    }
}