namespace NativeCode.Core.Platform.Serialization
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class PlatformJsonSerializer : IObjectSerializer, IStringSerializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformJsonSerializer" /> class.
        /// </summary>
        public PlatformJsonSerializer()
        {
            this.Settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelizeContractResolver(),
                DateParseHandling = DateParseHandling.DateTimeOffset,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                Formatting = Formatting.Indented,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };
            
            this.Serializer = JsonSerializer.Create(this.Settings);
        }
        
        protected JsonSerializer Serializer { get; }

        protected JsonSerializerSettings Settings { get; }

        T IObjectSerializer.Destructure<T>(JObject json)
        {
            return this.Serializer.Deserialize<T>(json.CreateReader());
        }

        JObject IObjectSerializer.Structure<T>(T value)
        {
            return JObject.FromObject(value, this.Serializer);
        }

        T IStringSerializer.Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, this.Settings);
        }

        string IStringSerializer.Serialize<T>(T instance)
        {
            return JsonConvert.SerializeObject(instance, this.Settings);
        }
    }
}