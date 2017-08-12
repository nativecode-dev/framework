namespace NativeCode.Core.Platform.Serialization
{
    using Newtonsoft.Json.Serialization;

    public class LowerScoreContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.Replace(" ", "_");
        }
    }
}