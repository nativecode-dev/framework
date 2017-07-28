namespace Cavern.Data.Scraping
{
    using System;
    using System.Collections.Generic;
    using NativeCode.Core.Data;

    public class ScraperTaskDefinition : Entity<Guid>
    {
        public ScraperProfile Profile { get; set; }

        public List<ScraperTask> Tasks { get; set; } = new List<ScraperTask>();
    }
}