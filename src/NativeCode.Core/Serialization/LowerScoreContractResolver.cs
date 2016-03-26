namespace NativeCode.Core.Serialization
{
    using Humanizer;

    using Newtonsoft.Json.Serialization;

    public class LowerScoreContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.Humanize(LetterCasing.LowerCase).Replace(" ", "_");
        }
    }
}