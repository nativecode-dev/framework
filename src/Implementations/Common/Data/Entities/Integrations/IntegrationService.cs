namespace Common.Data.Entities.Integrations
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class IntegrationService : Entity<int>
    {
        [Required]
        [MaxLength(CoreAnnotations.TextString)]
        [DataType(DataType.Url)]
        public string BaseAddress { get; set; }

        [MaxLength(CoreAnnotations.TextString)]
        public string Description { get; set; }

        [Required]
        [MaxLength(CoreAnnotations.TextIdentifier)]
        public string HandlerAssemblyQualifiedName { get; set; }

        [Required]
        [MaxLength(CoreAnnotations.TextName)]
        public string Name { get; set; }

        public virtual List<IntegrationServiceProperty> Properties { get; set; } = new List<IntegrationServiceProperty>();
    }
}