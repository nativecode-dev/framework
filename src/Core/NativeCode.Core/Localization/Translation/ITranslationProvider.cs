namespace NativeCode.Core.Localization.Translation
{
    using System.Globalization;
    using JetBrains.Annotations;

    /// <summary>
    /// Provides a contract for retrieving translated strings.
    /// </summary>
    public interface ITranslationProvider
    {
        /// <summary>
        /// Gets the translation string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns>System.String.</returns>
        string GetString([NotNull] string key, [NotNull] CultureInfo cultureInfo);
    }
}