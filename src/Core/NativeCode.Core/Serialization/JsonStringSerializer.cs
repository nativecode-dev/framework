namespace NativeCode.Core.Serialization
{
    using Newtonsoft.Json;

    public class JsonStringSerializer : IStringSerializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonStringSerializer"/> class.
        /// </summary>
        public JsonStringSerializer()
        {
            this.Settings = new JsonSerializerSettings { Formatting = Formatting.Indented, ContractResolver = new LowerScoreContractResolver() };
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        protected JsonSerializerSettings Settings { get; private set; }

        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public string Serialize<T>(T instance)
        {
            return JsonConvert.SerializeObject(instance);
        }
    }
}