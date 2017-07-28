namespace Cavern.Data
{
    using System.Data.Entity;
    using NativeCode.Core.Dependencies.Attributes;
    using NativeCode.Core.Packages.EntityFramework;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Platform.Connections;
    using Scraping;
    using Security;

    [Dependency(typeof(IScraperContext))]
    public class ScraperContext : DbDataContext, IScraperContext
    {
        public ScraperContext(IConnectionStringProvider provider, IPlatform platform) : base(provider, platform)
        {
        }

        public IDbSet<ScraperQuery> Queries { get; set; }

        public IDbSet<ScraperProfile> Profiles { get; set; }

        public IDbSet<Scraper> Scrapers { get; set; }

        public IDbSet<ScraperTaskDefinition> TaskDefinitions { get; set; }

        public IDbSet<User> Users { get; set; }
    }
}
