namespace NativeCode.Core.Web.Caching
{
    using System;
    using System.Web;
    using Core.Caching;

    public class HttpRuntimeCache<T> : ICache<T>
    {
        public T Get(string key, T defaultValue = default(T))
        {
            return (T) HttpRuntime.Cache[key];
        }

        public bool Get(string key, out T value)
        {
            return (value = (T) HttpRuntime.Cache[key]).Equals(default(T)) == false;
        }

        public void Set(string key, T value, TimeSpan? duration = null)
        {
            if (duration.HasValue)
                HttpRuntime.Cache.Insert(key, value, null, DateTime.UtcNow.Add(duration.Value), TimeSpan.Zero);
            else
                HttpRuntime.Cache.Insert(key, value);
        }
    }
}