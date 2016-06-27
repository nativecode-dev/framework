namespace NativeCode.Core.Localization.Translation
{
    using System.Globalization;

    using JetBrains.Annotations;

    public interface ITranslator
    {
        string Translate([NotNull] string key);

        string Translate([NotNull] string key, [NotNull] CultureInfo cultureInfo);

        string TranslateFormat([NotNull] string key, params object[] parameters);

        string TranslateFormat([NotNull] string key, [NotNull] CultureInfo cultureInfo, params object[] parameters);

        string TranslateString([NotNull] string value);

        string TranslateString([NotNull] string value, [NotNull] CultureInfo cultureInfo);
    }
}