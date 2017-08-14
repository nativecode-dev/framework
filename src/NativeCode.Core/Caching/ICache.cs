namespace NativeCode.Core.Caching
{
    using System;

    /// <summary>
    /// Provides a contract for caching facilities.
    /// </summary>
    /// <typeparam name="T">The type to cache.</typeparam>
    public interface ICache<T>
    {
        /// <summary>
        /// Gets the value of the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Returns the cached value.</returns>
        T Get(string key, Func<T> defaultValue = null);

        /// <summary>
        /// Gets the value of the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if value retrieved, <c>false</c> otherwise.</returns>
        bool Get(string key, out T value);

        /// <summary>
        /// Sets the value for the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="duration">The duration.</param>
        void Set(string key, T value, TimeSpan? duration = default(TimeSpan?));
    }
}