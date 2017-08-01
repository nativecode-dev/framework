namespace Cavern.Data.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common;
    using JetBrains.Annotations;
    using NativeCode.Core.Data;

    public class Login : Entity<Guid>
    {
        [Required]
        public Guid ApiKey { get; [NotNull] set; } = Guid.NewGuid();

        [DataType(DataType.EmailAddress)]
        [Required]
        [MaxLength(256)]
        public string Email { get; [NotNull] set; }

        public bool Enabled { get; set; }

        public List<LoginHistory> Logins { get; set; } = new List<LoginHistory>();

        public LoginType LoginType { get; set; }

        [Required]
        public byte[] PasswordHash { get; [NotNull] set; }

        [Required]
        public byte[] SaltHash { get; [NotNull] set; }
    }
}
