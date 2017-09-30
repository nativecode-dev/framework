namespace NativeCode.Core.Platform.Serialization
{
    using Humanizer;
    using Newtonsoft.Json.Serialization;

    public class CamelizeContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.Camelize();
        }
    }
}