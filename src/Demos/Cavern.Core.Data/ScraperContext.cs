namespace Cavern.Core.Data
{
    using System.Data.Entity;
    using Entities.Scraping;
    using Entities.Security;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Platform.Connections;

    public class ScraperContext : NativeCode.Core.Packages.EntityFramework.DbDataContext
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
