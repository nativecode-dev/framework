namespace NativeCode.Core.Platform.Serialization
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class JsonStringSerializer : IStringSerializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonStringSerializer" /> class.
        /// </summary>
        public JsonStringSerializer()
        {
            this.Settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateParseHandling = DateParseHandling.DateTimeOffset,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                Formatting = Formatting.Indented
            };
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        protected JsonSerializerSettings Settings { get; }

        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, this.Settings);
        }

        public string Serialize<T>(T instance)
        {
            return JsonConvert.SerializeObject(instance, this.Settings);
        }
    }
}