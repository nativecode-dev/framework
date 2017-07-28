namespace Cavern.Data.Scraping
{
    using System;
    using NativeCode.Core.Data;

    /// <summary>
    /// Maps the semantic meaning of a field to its representation within
    /// the document.
    /// </summary>
    public class ScraperMapping : Entity<Guid>
    {
        public ScraperField Field { get; set; }

        public ScraperFormField FormField { get; set; }
    }
}