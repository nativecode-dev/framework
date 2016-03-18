namespace NativeCode.Core.Settings
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using JetBrains.Annotations;

    using NativeCode.Core.Dependencies.Attributes;
    using NativeCode.Core.Extensions;

    [IgnoreDependency("Settings must always be manually constructed for parameter variance.")]
    public abstract class Settings
    {
        public object this[string key]
        {
            get
            {
                return this.ReadValue<object>(key);
            }

            set
            {
                this.WriteValue(key, value, true);
            }
        }

        public IEnumerable<string> Keys => this.GetKeys();

        protected string PathSeparator { get; set; } = ".";

        public T GetValue<T>([NotNull] string name, T defaultValue = default(T))
        {
            if (this.IsPath(name))
            {
                var path = name.Split(this.PathSeparator);
                return this.ReadValue(path, defaultValue);
            }

            return this.ReadValue(name, defaultValue);
        }

        public void SetValue<T>([NotNull] string name, T value, bool overwrite = true)
        {
            if (this.IsPath(name))
            {
                var path = name.Split(this.PathSeparator);
                this.WriteValue(path, value, overwrite);
            }
        }

        public abstract void Load([NotNull] string content, Encoding encoding);

        public abstract void Load([NotNull] Stream stream);

        public abstract void Save([NotNull] Stream stream);

        protected abstract IEnumerable<string> GetKeys();

        protected abstract T ReadValue<T>([NotNull] string name, T defaultValue = default(T));

        protected abstract T ReadValue<T>([NotNull] string[] path, T defaultValue = default(T));

        protected abstract void WriteValue<T>([NotNull] string name, T value, bool overwrite);

        protected abstract void WriteValue<T>([NotNull] string[] path, T value, bool overwrite);

        protected bool IsPath([NotNull] string value)
        {
            // NOTE:
            // We don't check if it's zero because a property could start with the separator.
            // For example, ".name" is not a path but ".name.other" is.
            return value.IndexOf(this.PathSeparator, StringComparison.Ordinal) > 0;
        }
    }
}