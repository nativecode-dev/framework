namespace NativeCode.Core.Localization.Translation
{
    using System.Globalization;

    using JetBrains.Annotations;

    public interface ITranslationProvider
    {
        string GetString([NotNull] string key, [NotNull] CultureInfo cultureInfo);
    }
}