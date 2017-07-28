namespace Cavern.Core.Data.Entities.Security
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using JetBrains.Annotations;
    using NativeCode.Core.Data;

    public class User : Entity<Guid>
    {
        [StringLength(256, MinimumLength = 2)]
        public string DisplayName { get; set; }

        public Login Login { get; [NotNull] set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        [MaxLength(256)]
        public string Username { get; [NotNull] set; }
    }
}