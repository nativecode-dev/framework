namespace NativeCode.Core.Platform.Serialization
{
    using JetBrains.Annotations;
    using Newtonsoft.Json.Linq;

    public interface IObjectSerializer
    {
        T Destructure<T>([NotNull] JObject json);

        JObject Structure<T>([NotNull] T value);
    }
}