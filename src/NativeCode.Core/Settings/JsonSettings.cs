namespace NativeCode.Core.Settings
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Platform.Serialization;

    public class JsonSettings : Settings
    {
        private readonly IStringSerializer serializer;

        public JsonSettings(IStringSerializer serializer)
        {
            this.serializer = serializer;
            this.ObjectInstance = new JObject();
        }

        public JsonSettings(JsonSettings instance)
        {
            this.ObjectInstance = instance.ObjectInstance;
        }

        protected JObject ObjectInstance { get; private set; }

        public void Deserialize(string content)
        {
            this.ObjectInstance = this.serializer.Deserialize<JObject>(content);
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
                var json = new JsonSerializer();
                this.ObjectInstance = (JObject) json.Deserialize(reader);
            }
        }

        public override void Save(Stream stream)
        {
            using (var writer = new JsonTextWriter(new StreamWriter(stream)))
            {
                var json = new JsonSerializer();
                json.Serialize(writer, this.ObjectInstance);
            }
        }

        public string Serialize()
        {
            return this.serializer.Serialize(this.ObjectInstance);
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
            var current = (JToken) this.ObjectInstance;

            foreach (var name in path)
            {
                if (current?[name] == null)
                {
                    return defaultValue;
                }

                current = current[name];
            }

            return JsonSettings.ReadTokenValue(current, defaultValue);
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
                    var jobject = new JObject();
                    current.Add(name, jobject);
                }

                current = (JObject) current[name];
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
            string FlattenName(string p, string k)
            {
                return p == null ? k : string.Join(this.PathSeparator, p, k);
            }

            foreach (var child in container)
            {
                var key = child.Key;
                var value = child.Value;

                var name = FlattenName(path, key);
                keys.Add(name);

                if (value.Type == JTokenType.Object)
                {
                    this.FlattenKeys((JObject) value, keys, name);
                }
            }
        }
    }
}