namespace Cavern.Services
{
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.DotNet.Platform;
    using NativeCode.Core.Packages.Unity;
    using NativeCode.Core.Platform;

    public class ScraperService : ScraperKernel
    {
        public ScraperService() : base(CreateDefaultPlatform())
        {
        }

        private static IPlatform CreateDefaultPlatform()
        {
            IDependencyContainer container = new UnityDependencyContainer();

            try
            {
                return new DotNetPlatform(container);
            }
            catch
            {
                container.Dispose();
                throw;
            }
        }
    }
}