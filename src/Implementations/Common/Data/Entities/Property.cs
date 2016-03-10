namespace Common.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public abstract class Property : Entity<long>
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        public string Value { get; set; }
    }
}