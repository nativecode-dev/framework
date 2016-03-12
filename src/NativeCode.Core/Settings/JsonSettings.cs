namespace NativeCode.Core.Settings
{
    using System.IO;
    using System.Linq;
    using System.Text;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class JsonSettings : Settings
    {
        protected JObject ObjectInstance { get; private set; } = new JObject();

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
            var serializer = new JsonSerializer();
            using (var reader = new JsonTextReader(new StreamReader(stream)))
            {
                this.ObjectInstance = (JObject)serializer.Deserialize(reader);
            }
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

        protected override void WriteValue<T>(string name, T value)
        {
            this.WriteValue(new string[0], value);
        }

        protected override void WriteValue<T>(string[] path, T value)
        {
            var current = this.ObjectInstance;
            var last = path[path.Length - 1];

            foreach (var name in path)
            {
                if (name == last)
                {
                    current.Add(name, new JValue(value));
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
    }
}