namespace NativeCode.Core.Settings
{
    using System;
    using System.IO;
    using System.Text;

    using JetBrains.Annotations;

    using NativeCode.Core.Dependencies.Attributes;
    using NativeCode.Core.Extensions;

    [IgnoreDependency("Settings readers must always be manually constructed for parameter variance.")]
    public abstract class Settings
    {
        protected string PathSeparator { get; set; } = ".";

        public object this[string key]
        {
            get
            {
                return this.ReadValue<object>(key);
            }

            set
            {
                this.WriteValue(key, value);
            }
        }

        public T GetValue<T>([NotNull] string name, T defaultValue = default(T))
        {
            if (this.IsPath(name))
            {
                var path = name.Split(this.PathSeparator);
                return this.ReadValue(path, defaultValue);
            }

            return this.ReadValue(name, defaultValue);
        }

        public void SetValue<T>([NotNull] string name, T value)
        {
            if (this.IsPath(name))
            {
                var path = name.Split(this.PathSeparator);
                this.WriteValue(path, value);
            }
        }

        public abstract void Load([NotNull] string content, Encoding encoding);

        public abstract void Load([NotNull] Stream stream);

        protected abstract T ReadValue<T>([NotNull] string name, T defaultValue = default(T));

        protected abstract T ReadValue<T>([NotNull] string[] path, T defaultValue = default(T));

        protected abstract void WriteValue<T>([NotNull] string name, T value);

        protected abstract void WriteValue<T>([NotNull] string[] path, T value);

        protected bool IsPath([NotNull] string value)
        {
            // NOTE:
            // We don't check if it's zero because a property could start with the separator.
            // For example, ".name" is not a path but ".name.other" is.
            return value.IndexOf(this.PathSeparator, StringComparison.Ordinal) > 0;
        }
    }
}