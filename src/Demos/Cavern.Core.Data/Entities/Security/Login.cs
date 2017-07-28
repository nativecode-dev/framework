namespace Cavern.Core.Data.Entities.Security
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using JetBrains.Annotations;
    using NativeCode.Core.Data;

    public class Login : Entity<Guid>
    {
        [Required]
        public Guid ApiKey { get; [NotNull] set; } = Guid.NewGuid();

        public bool Enabled { get; set; }

        [Required]
        public byte[] PasswordHash { get; [NotNull] set; }

        [Required]
        public byte[] SaltHash { get; [NotNull] set; }
    }
}
