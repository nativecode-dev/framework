namespace NativeCode.Core.Localization.Translation
{
    using System.Globalization;

    public class TranslationProvider : ITranslationProvider
    {
        public string GetString(string key, CultureInfo culture)
        {
            return key;
        }
    }
}