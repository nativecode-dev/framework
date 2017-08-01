namespace Cavern.Data.Scraping
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common;
    using NativeCode.Core.Data;

    public class ScraperFormField : Entity<Guid>
    {
        [MaxLength(128)]
        public string ElementId { get; set; }

        [MaxLength(128)]
        public string ElementName { get; set; }

        [MaxLength(256)]
        [Required]
        public string ElementSelector { get; set; }

        public ScraperFormFieldType ElementType { get; set; }

        [MaxLength(256)]
        [Required]
        public string ValueSelector { get; set; }

        [MaxLength(2048)]
        public string ValueSelectorRegex { get; set; }
    }
}