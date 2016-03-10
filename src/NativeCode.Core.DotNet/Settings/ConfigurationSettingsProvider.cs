namespace NativeCode.Core.DotNet.Settings
{
    using System.Configuration;

    using SettingsProvider = NativeCode.Core.Settings.SettingsProvider;

    public class ConfigurationSettingsProvider : SettingsProvider
    {
        public ConfigurationSettingsProvider()
        {
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                this.SetValue(key, ConfigurationManager.AppSettings[key]);
            }

            foreach (ConnectionStringSettings setting in ConfigurationManager.ConnectionStrings)
            {
                this.SetValue(setting.Name, setting.ConnectionString);
            }
        }
    }
}