namespace NativeCode.Core.Settings
{
    using System.Collections.Generic;

    public interface ISettingsProvider
    {
        string ApplicationName { get; set; }

        string Environment { get; set; }

        IEnumerable<string> Keys { get; }

        IEnumerable<string> Values { get; }

        bool ContainsKey(string key);

        string GetValue(string key, string defaultValue = default(string));

        void Merge(ISettingsProvider source);

        void SetValue(string key, string value);
    }
}