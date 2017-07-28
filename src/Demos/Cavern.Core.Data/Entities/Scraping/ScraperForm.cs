namespace Cavern.Core.Data.Entities.Scraping
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using NativeCode.Core.Data;

    /// <summary>
    /// Scraper forms represent HTML forms and their related elements. We also
    /// keep track of mapping the relevant <see cref="ScraperFormField"/> to
    /// its semantic equivalent <see cref="ScraperField"/>.
    /// </summary>
    public class ScraperForm : Entity<Guid>
    {
        public List<ScraperFormField> Fields { get; set; } = new List<ScraperFormField>();

        [Required]
        public string FormHtml { get; set; }

        [MaxLength(64)]
        public string FormId { get; set; }

        [MaxLength(128)]
        public string FormName { get; set; }

        [MaxLength(1024)]
        public string FormUrl { get; set; }

        public ScraperFormType FormType { get; set; }

        public List<ScraperMapping> Mappings { get; set; } = new List<ScraperMapping>();
    }
}