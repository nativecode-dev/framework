namespace NativeCode.Core.Settings
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Serialization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class JsonSettings : Settings
    {
        public JsonSettings()
        {
            this.ObjectInstance = new JObject();
        }

        public JsonSettings(JsonSettings instance)
        {
            this.ObjectInstance = instance.ObjectInstance;
        }

        protected JObject ObjectInstance { get; private set; }

        public void Deserialize(string content)
        {
            var serializer = DependencyLocator.Resolver.Resolve<IStringSerializer>();
            this.ObjectInstance = serializer.Deserialize<JObject>(content);
        }

        public override void Load(string content, Encoding encoding)
        {
            var bytes = encoding.GetBytes(content);
            using (var reader = new MemoryStream(bytes))
            {
                this.Load(reader);
            }
        }

        public override void Load(Stream stream)
        {
            using (var reader = new JsonTextReader(new StreamReader(stream)))
            {
                var serializer = new JsonSerializer();
                this.ObjectInstance = (JObject)serializer.Deserialize(reader);
            }
        }

        public override void Save(Stream stream)
        {
            using (var writer = new JsonTextWriter(new StreamWriter(stream)))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, this.ObjectInstance);
            }
        }

        public string Serialize()
        {
            var serializer = DependencyLocator.Resolver.Resolve<IStringSerializer>();
            return serializer.Serialize(this.ObjectInstance);
        }

        protected override IEnumerable<string> GetKeys()
        {
            var keys = new List<string>();

            this.FlattenKeys(this.ObjectInstance, keys);

            return keys;
        }

        protected override T ReadValue<T>(string name, T defaultValue = default(T))
        {
            return this.ReadValue<T>(new[] { name });
        }

        protected override T ReadValue<T>(string[] path, T defaultValue = default(T))
        {
            var current = (JToken)this.ObjectInstance;

            foreach (var name in path)
            {
                if (current?[name] == null)
                {
                    return defaultValue;
                }

                current = current[name];
            }

            return ReadTokenValue(current, defaultValue);
        }

        protected override void WriteValue<T>(string name, T value, bool overwrite)
        {
            this.WriteValue(new string[0], value, overwrite);
        }

        protected override void WriteValue<T>(string[] path, T value, bool overwrite)
        {
            var current = this.ObjectInstance;
            var terminator = path[path.Length - 1];

            foreach (var name in path)
            {
                if (name == terminator)
                {
                    if (current[name] == null)
                    {
                        current.Add(name, new JValue(value));
                    }
                    else if (overwrite)
                    {
                        current[name] = new JValue(value);
                    }

                    return;
                }

                if (current[name] == null)
                {
                    var container = new JObject();
                    current.Add(name, container);
                }

                current = (JObject)current[name];
            }
        }

        private static T ReadTokenValue<T>(JToken token, T defaultValue = default(T))
        {
            if (token == null)
            {
                return defaultValue;
            }

            return token.ToObject<T>();
        }

        private void FlattenKeys(JObject container, ICollection<string> keys, string path = default(string))
        {
            Func<string, string, string> build = (p, k) => p == null ? k : string.Join(this.PathSeparator, p, k);

            foreach (var child in container)
            {
                var key = child.Key;
                var value = child.Value;

                var name = build(path, key);
                keys.Add(name);

                if (value.Type == JTokenType.Object)
                {
                    this.FlattenKeys((JObject)value, keys, name);
                }
            }
        }
    }
}