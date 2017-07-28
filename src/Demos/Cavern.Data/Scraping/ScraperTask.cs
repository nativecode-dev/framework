namespace Cavern.Data.Scraping
{
    using System;
    using NativeCode.Core.Data;

    public class ScraperTask : Entity<Guid>
    {
        public ScraperTaskDefinition Definition { get; set; }

        public DateTimeOffset Started { get; set; }

        public DateTimeOffset Stopped { get; set; }
    }
}