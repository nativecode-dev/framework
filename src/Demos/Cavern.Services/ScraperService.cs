namespace Cavern.Services
{
    using JetBrains.Annotations;
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.DotNet.Platform;
    using NativeCode.Core.Packages.Unity;
    using NativeCode.Core.Platform;

    public class ScraperService : ScraperKernel
    {
        public ScraperService() : base(CreateDefaultPlatform())
        {
        }

        [NotNull]
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