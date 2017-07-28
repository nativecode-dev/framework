namespace Cavern.Data
{
    using System.Data.Entity;
    using NativeCode.Core.Data;
    using Scraping;
    using Security;

    public interface IScraperContext : IDataContext
    {
        IDbSet<ScraperQuery> Queries { get; set; }

        IDbSet<ScraperProfile> Profiles { get; set; }

        IDbSet<Scraper> Scrapers { get; set; }

        IDbSet<ScraperTaskDefinition> TaskDefinitions { get; set; }

        IDbSet<User> Users { get; set; }
    }
}