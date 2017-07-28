namespace Cavern.Core.Data.Entities.Scraping
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using NativeCode.Core.Data;

    /// <summary>
    /// Scraper profiles encapsulate the page definition of its forms
    /// and elements.
    /// </summary>
    public class ScraperProfile : Entity<Guid>
    {
        public List<ScraperForm> Forms { get; set; } = new List<ScraperForm>();

        [MaxLength(128)]
        [Required]
        public string Name { get; set; }

        public Scraper Scraper { get; set; }
    }
}