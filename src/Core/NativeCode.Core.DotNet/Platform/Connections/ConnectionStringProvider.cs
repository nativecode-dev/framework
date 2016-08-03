namespace NativeCode.Core.DotNet.Platform.Connections
{
    using System;
    using System.Configuration;

    using NativeCode.Core.Platform.Connections;

    public class ConnectionStringProvider : IConnectionStringProvider
    {
        public ConnectionString GetConnectionString<T>()
        {
            return this.GetConnectionString(typeof(T).Name);
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

        public virtual ConnectionString GetDefaultConnectionString()
        {
            return this.GetConnectionString("Default");
        }
    }
}