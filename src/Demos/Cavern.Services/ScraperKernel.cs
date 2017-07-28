namespace Cavern.Services
{
    using NativeCode.Core.Platform;
    using NativeCode.Core.Settings;

    public class ScraperKernel : Application
    {
        public ScraperKernel(IPlatform platform) : base(platform, new JsonSettings())
        {
        }
    }
}
