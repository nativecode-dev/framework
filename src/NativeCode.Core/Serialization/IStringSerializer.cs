namespace NativeCode.Core.Serialization
{
    using JetBrains.Annotations;

    public interface IStringSerializer
    {
        T Deserialize<T>([NotNull] string value);

        string Serialize<T>([NotNull] T instance);
    }
}