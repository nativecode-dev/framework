namespace NativeCode.Core.Serialization
{
    using Humanizer;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

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

    public class LowerScoreContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.Humanize(LetterCasing.LowerCase).Replace(" ", "_");
        }
    }
}