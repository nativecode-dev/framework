namespace NativeCode.Core.Settings
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class SettingsProvider : ISettingsProvider
    {
        private readonly ConcurrentDictionary<string, string> settings = new ConcurrentDictionary<string, string>();

        public string ApplicationName
        {
            get
            {
                return this.GetValue("app.name");
            }
            set
            {
                this.SetValue("app.name", value);
            }
        }

        public string Environment
        {
            get
            {
                return this.GetValue("app.environment");
            }
            set
            {
                this.SetValue("app.environment", value);
            }
        }

        public IEnumerable<string> Keys => this.settings.Keys;

        public IEnumerable<string> Values => this.settings.Values;

        public bool ContainsKey(string key)
        {
            return this.settings.ContainsKey(key);
        }

        public string GetValue(string key, string defaultValue = null)
        {
            if (this.settings.ContainsKey(key))
            {
                return this.settings[key];
            }

            return defaultValue;
        }

        public void Merge(ISettingsProvider source)
        {
            foreach (var key in source.Keys)
            {
                if (this.GetValue(key) == null)
                {
                    this.SetValue(key, source.GetValue(key));
                }
            }
        }

        public void SetValue(string key, string value)
        {
            this.settings.AddOrUpdate(key, k => value, (k, v) => value);
        }
    }
}