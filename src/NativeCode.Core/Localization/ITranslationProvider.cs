namespace NativeCode.Core.Localization
{
    using System.Globalization;

    using JetBrains.Annotations;

    public interface ITranslationProvider
    {
        string GetString([NotNull] string key, [NotNull] CultureInfo cultureInfo);
    }
}