namespace NativeCode.Core.Localization.Translation
{
    using JetBrains.Annotations;

    /// <summary>
    /// Provides a contract to translate object string properties.
    /// </summary>
    public interface IObjectTranslator
    {
        /// <summary>
        /// Translates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        void Translate([NotNull] object instance);

        /// <summary>
        /// Translates the specified instance.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="instance">The instance.</param>
        void Translate<T>([NotNull] T instance);
    }
}