namespace NativeCode.Core.Localization.Translation
{
    using System.Globalization;

    public class Translator : ITranslator
    {
        private readonly ITranslationProvider provider;

        public Translator(ITranslationProvider provider)
        {
            this.provider = provider;
        }

        public string Translate(string key)
        {
            return this.Translate(key, CultureInfo.CurrentUICulture);
        }

        public string Translate(string key, CultureInfo cultureInfo)
        {
            return this.provider.GetString(key, cultureInfo);
        }

        public string TranslateFormat(string key, params object[] parameters)
        {
            return this.TranslateFormat(key, CultureInfo.CurrentUICulture, parameters);
        }

        public string TranslateFormat(string key, CultureInfo cultureInfo, params object[] parameters)
        {
            return string.Format(this.provider.GetString(key, cultureInfo), parameters);
        }

        public string TranslateString(string value)
        {
            return this.TranslateString(value, CultureInfo.CurrentUICulture);
        }

        public string TranslateString(string value, CultureInfo cultureInfo)
        {
            return this.provider.GetString(value, cultureInfo);
        }
    }
}