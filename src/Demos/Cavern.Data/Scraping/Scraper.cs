namespace Cavern.Data.Scraping
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using NativeCode.Core.Data;

    /// <summary>
    /// Defines a scraper and a hash for easy lookup.
    /// </summary>
    /// <remarks>
    /// Hash implementation should be some combination of the
    /// URL host, port, path, etc. I would suggest ignoring the
    /// use of query strings.
    /// </remarks>
    public class Scraper : Entity<Guid>
    {
        [DataType(DataType.Url)]
        [Required]
        [MaxLength(1024)]
        public string Url { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(512)]
        [Required]
        public string UrlHash { get; set; }
    }
}
