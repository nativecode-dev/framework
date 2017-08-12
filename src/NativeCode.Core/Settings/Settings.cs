namespace NativeCode.Core.Settings
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using Extensions;
    using JetBrains.Annotations;

    public abstract class Settings
    {
        protected Settings()
        {
            this.Prefix = this.GetType().Namespace;
        }

        public IEnumerable<string> Keys => this.GetKeys();

        protected string PathSeparator { get; set; } = ".";

        protected string Prefix { get; set; }

        public object this[string key]
        {
            get => this.ReadValue<object>(key);
            set => this.WriteValue(key, value, true);
        }

        public T GetValue<T>([NotNull] string name, T defaultValue = default(T), bool saveWhenDefault = false)
        {
            if (this.IsPath(name))
            {
                var path = name.Split(this.PathSeparator);

                if (saveWhenDefault)
                    this.WriteValue(path, defaultValue, true);

                return this.ReadValue(path, defaultValue);
            }

            if (saveWhenDefault)
                this.WriteValue(name, defaultValue, true);

            return this.ReadValue(name, defaultValue);
        }

        public abstract void Load([NotNull] string content, Encoding encoding);

        public abstract void Load([NotNull] Stream stream);

        public abstract void Save([NotNull] Stream stream);

        public void SetValue<T>([NotNull] string name, T value, bool overwrite = true)
        {
            if (this.IsPath(name))
            {
                var path = name.Split(this.PathSeparator);
                this.WriteValue(path, value, overwrite);
            }
        }

        protected abstract IEnumerable<string> GetKeys();

        protected T GetMemberValue<T>(T defaultValue = default(T), [CallerMemberName] string name = null,
            bool saveWhenDefault = false)
        {
            var key = this.GetPrefixKey(name);
            return this.GetValue(key, defaultValue, saveWhenDefault);
        }

        protected string GetPrefixKey(string name)
        {
            if (string.IsNullOrWhiteSpace(this.Prefix))
                return name;

            return $"{this.Prefix}.{name}";
        }

        protected bool IsPath([NotNull] string value)
        {
            // NOTE:
            // We don't check if it's zero because a property could start with the separator.
            // For example, ".name" is not a path but ".name.other" is.
            return value.IndexOf(this.PathSeparator, StringComparison.Ordinal) > 0;
        }

        protected abstract T ReadValue<T>([NotNull] string name, T defaultValue = default(T));

        protected abstract T ReadValue<T>([NotNull] string[] path, T defaultValue = default(T));

        protected void SetMemberValue<T>(T value, [CallerMemberName] string name = null)
        {
            var key = this.GetPrefixKey(name);
            this.SetValue(key, value);
        }

        protected abstract void WriteValue<T>([NotNull] string name, T value, bool overwrite);

        protected abstract void WriteValue<T>([NotNull] string[] path, T value, bool overwrite);
    }
}