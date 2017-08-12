namespace NativeCode.Core.Platform.Connections
{
    using System.Collections.Generic;
    using System.Linq;

    public class SettingsConnectionStringProvider : IConnectionStringProvider
    {
        private const string DefaultConnectionStringName = "Default";

        public SettingsConnectionStringProvider(IApplication application)
        {
            this.Application = application;
        }

        protected IApplication Application { get; }

        public virtual ConnectionString GetConnectionString<T>()
        {
            return this.GetConnectionString(typeof(T).Name);
        }

        public virtual ConnectionString GetConnectionString(string name)
        {
            if (this.Application.SettingsObject.Keys.Contains(name))
                return new ConnectionString(this.Application.SettingsObject.GetValue<string>(name));

            // TODO: Need better exception.
            throw new KeyNotFoundException(name);
        }

        public virtual ConnectionString GetDefaultConnectionString()
        {
            return this.GetConnectionString(DefaultConnectionStringName);
        }
    }
}