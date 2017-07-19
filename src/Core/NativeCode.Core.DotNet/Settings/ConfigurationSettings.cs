namespace NativeCode.Core.DotNet.Settings
{
    using System.Configuration;
    using Core.Platform.Connections;
    using Core.Settings;

    public class ConfigurationSettings : JsonSettings
    {
        public ConfigurationSettings()
            : this(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None))
        {
        }

        public ConfigurationSettings(Configuration configuration)
        {
            this.Configuration = configuration;
            this.Prefix = null;
            this.PopulateAppSettings();
            this.PopulateConnectionStrings();
        }

        protected Configuration Configuration { get; }

        private void PopulateAppSettings()
        {
            var appSettings = this.GetAppSettings();

            foreach (var key in appSettings.Settings.AllKeys)
            {
                var path = $"AppSettings.{key}";
                this.WriteValue(path, appSettings.Settings[key].Value, true);
            }
        }

        private void PopulateConnectionStrings()
        {
            var connectionStrings = this.GetConnectionStrings();

            foreach (ConnectionStringSettings value in connectionStrings)
            {
                var path = $"ConnectionStrings.{value.Name}";
                dynamic connectionString = new ConnectionString(value.ConnectionString);
                connectionString.ProviderName = value.ProviderName;
                this.WriteValue(path, connectionString, true);
            }
        }

        private AppSettingsSection GetAppSettings()
        {
            return this.Configuration.AppSettings;
        }

        private ConnectionStringSettingsCollection GetConnectionStrings()
        {
            return this.Configuration.ConnectionStrings.ConnectionStrings;
        }
    }
}