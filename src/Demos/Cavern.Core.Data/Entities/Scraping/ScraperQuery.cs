namespace Cavern.Core.Data.Entities.Scraping
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using NativeCode.Core.Data;

    public class ScraperQuery : Entity<Guid>
    {
        public ScraperProfile Profile { get; set; }

        public int ResultCount { get; set; }

        [Required]
        public string Results { get; set; }

        [MaxLength(256)]
        [Required]
        public string ResultSelector { get; set; }
    }
}
