using System.Globalization;
using System.Resources;
using JetBrains.Annotations;

namespace NativeCode.Core.Localization.Translation
{
    public class ResourceTranslatorProvider : ITranslationProvider
    {
        private readonly ResourceManager resourceManager;
        
        public ResourceTranslatorProvider(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }

        public string GetString([NotNull] string key, [NotNull] CultureInfo cultureInfo)
        {
            return this.resourceManager.GetString(key);
        }
    }
}
