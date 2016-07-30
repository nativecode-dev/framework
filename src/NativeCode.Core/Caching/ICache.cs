namespace NativeCode.Core.Caching
{
    using System;

    public interface ICache<T>
    {
        T Get(string key, T defaultValue = default(T));

        bool Get(string key, out T value);

        void Set(string key, T value, TimeSpan? duration = default(TimeSpan?));
    }
}