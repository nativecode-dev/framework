namespace NativeCode.Core.Serialization
{
    using Newtonsoft.Json;

    public class JsonNetStringSerializer : IStringSerializer
    {
        public JsonNetStringSerializer()
        {
            this.Settings = new JsonSerializerSettings { Formatting = Formatting.Indented, ContractResolver = new LowerScoreContractResolver() };
        }

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