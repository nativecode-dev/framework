namespace NativeCode.Core.Localization.Translation
{
    using System.Globalization;
    using JetBrains.Annotations;

    /// <summary>
    /// Provides a contract for retrieving translated strings.
    /// </summary>
    public interface ITranslator
    {
		/// <summary>
		/// Translates the specified key using the provided format.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="parameters">The parameters.</param>
		/// <returns>System.String.</returns>
		string TranslateFormat([NotNull] string key, params object[] parameters);

		/// <summary>
		/// Translates the specified key using the provided format.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="cultureInfo">The culture information.</param>
		/// <param name="parameters">The parameters.</param>
		/// <returns>System.String.</returns>
		string TranslateFormat([NotNull] string key, [NotNull] CultureInfo cultureInfo, params object[] parameters);

		/// <summary>
		/// Translates the specified key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>System.String.</returns>
		string TranslateKey([NotNull] string key);

        /// <summary>
        /// Translates the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns>System.String.</returns>
        string TranslateKey([NotNull] string key, [NotNull] CultureInfo cultureInfo);

        /// <summary>
        /// Translates the specified string and replaces any translation tokens.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        string TranslateString([NotNull] string value);

        /// <summary>
        /// Translates the specified string and replaces any translation tokens.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns>System.String.</returns>
        string TranslateString([NotNull] string value, [NotNull] CultureInfo cultureInfo);
    }
}