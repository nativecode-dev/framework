namespace NativeCode.Core.Caching
{
    using System;
    using System.Collections.Concurrent;

    /// <summary>
    /// Provides a simple and safe caching mechanism using a <c>ConcurrentDictionary</c>.
    /// </summary>
    /// <typeparam name="T">The type of the item to cache.</typeparam>
    /// <seealso cref="NativeCode.Core.Caching.ICache{T}" />
    public class SafeCache<T> : ICache<T>
    {
        private readonly ConcurrentDictionary<string, T> cached = new ConcurrentDictionary<string, T>();

        /// <summary>
        /// Gets the value of the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Returns the cached value.</returns>
        public T Get(string key, T defaultValue = default(T))
        {
            T value;

            if (this.cached.TryGetValue(key, out value))
            {
                return value;
            }

#pragma warning disable RECS0017 // Possible compare of value type with 'null'
            if (defaultValue != null)
#pragma warning restore RECS0017 // Possible compare of value type with 'null'
            {
                this.Set(key, defaultValue);
            }

            return defaultValue;
        }

        /// <summary>
        /// Gets the value of the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if value retrieved, <c>false</c> otherwise.</returns>
        public bool Get(string key, out T value)
        {
            return this.cached.TryGetValue(key, out value);
        }

        /// <summary>
        /// Sets the value for the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="duration">The duration.</param>
        public void Set(string key, T value, TimeSpan? duration = default(TimeSpan?))
        {
            this.cached.AddOrUpdate(key, k => value, (k, v) => value);
        }
    }
}