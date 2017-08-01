namespace Cavern.Services
{
    using JetBrains.Annotations;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Settings;

    public class ScraperKernel : Application
    {
        public ScraperKernel([NotNull] IPlatform platform) : base(platform, new JsonSettings())
        {
        }
    }
}
