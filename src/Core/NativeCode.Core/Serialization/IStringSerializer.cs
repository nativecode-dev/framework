namespace NativeCode.Core.Serialization
{
    using JetBrains.Annotations;

    /// <summary>
    /// Provides a simple JSON string converter.
    /// </summary>
    public interface IStringSerializer
    {
        /// <summary>
        /// Deserializes the specified value.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>Returns deserialized object.</returns>
        T Deserialize<T>([NotNull] string value);

        /// <summary>
        /// Serializes the specified instance.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns>Returns a serialized string representation.</returns>
        string Serialize<T>([NotNull] T instance);
    }
}