namespace Cavern.Data.Scraping
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common;
    using NativeCode.Core.Data;

    /// <summary>
    /// Represents a semantic meaning that translates fHTML form
    /// input fields into something with meaning, that can be 
    /// used to drive the scraping logic.
    /// </summary>
    public class ScraperField : Entity<Guid>
    {
        [MaxLength(128)]
        [Required]
        public string Name { get; set; }

        [MaxLength(128)]
        public string SemanticMeaning { get; set; }

        public ScraperValueType ValueType { get; set; }
    }
}