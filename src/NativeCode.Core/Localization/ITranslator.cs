namespace NativeCode.Core.Localization
{
    using System.Globalization;

    using JetBrains.Annotations;

    public interface ITranslator
    {
        string Translate([NotNull] string key);

        string Translate([NotNull] string key, [NotNull] CultureInfo cultureInfo);

        string TranslateFormat([NotNull] string key, params object[] parameters);

        string TranslateFormat([NotNull] string key, [NotNull] CultureInfo cultureInfo, params object[] parameters);
    }
}