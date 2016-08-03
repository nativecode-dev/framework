namespace NativeCode.Core.Serialization
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