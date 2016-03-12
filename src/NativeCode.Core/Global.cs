namespace NativeCode.Core
{
    using System;

    using NativeCode.Core.Settings;

    public static class Global
    {
        private static readonly Lazy<JsonSettings> SettingsInstance = new Lazy<JsonSettings>(() => new JsonSettings(), true);

        public static Settings.Settings Settings => SettingsInstance.Value;
    }
}