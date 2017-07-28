namespace Cavern.Data.Security
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
    }
}