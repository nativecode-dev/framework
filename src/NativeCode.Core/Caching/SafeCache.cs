namespace NativeCode.Core.Caching
{
    using System;
    using System.Collections.Concurrent;

    public class SafeCache<T> : ICache<T>
    {
        private readonly ConcurrentDictionary<string, T> cached = new ConcurrentDictionary<string, T>();

        public T Get(string key, T defaultValue = default(T))
        {
            T value;

            if (this.cached.TryGetValue(key, out value))
            {
                return value;
            }

            if (defaultValue != null)
            {
                this.Set(key, defaultValue);
            }

            return defaultValue;
        }

        public bool Get(string key, out T value)
        {
            return this.cached.TryGetValue(key, out value);
        }

        public void Set(string key, T value, TimeSpan? duration = default(TimeSpan?))
        {
            this.cached.AddOrUpdate(key, k => value, (k, v) => value);
        }
    }
}