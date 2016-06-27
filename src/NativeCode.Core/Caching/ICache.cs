namespace NativeCode.Core.Caching
{
    public interface ICache<T>
    {
        T Get(string key, T defaultValue = default(T));

        void Set(string key, T value);
    }
}