namespace NativeCode.Core.Localization.Translation
{
    /// <summary>
    /// Provides a contract to translate object string properties.
    /// </summary>
    public interface IObjectTranslator
    {
        /// <summary>
        /// Translates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        void Translate(object instance);

        /// <summary>
        /// Translates the specified instance.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="instance">The instance.</param>
        void Translate<T>(T instance);
    }
}