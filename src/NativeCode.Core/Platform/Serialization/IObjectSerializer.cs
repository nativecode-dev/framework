namespace NativeCode.Core.Platform.Serialization
{
    using JetBrains.Annotations;
    using Newtonsoft.Json.Linq;

    public interface IObjectSerializer
    {
        [NotNull]
        T Destructure<T>([NotNull] JObject json);

        [NotNull]
        JObject Structure<T>([NotNull] T value);
    }
}