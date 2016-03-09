namespace Common.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class Token : Entity<Guid>
    {
        [Required]
        [MaxLength(256)]
        public string KeyAssociation { get; set; }

        public DateTimeOffset? ExpirationDate { get; set; }
    }
}