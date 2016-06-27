namespace Common.Data.Entities.Security
{
    using System;
    using System.Collections.Generic;

    using NativeCode.Core.Data;

    public class Token : Entity<Guid>
    {
        public Account Account { get; set; }

        public DateTimeOffset? ExpirationDate { get; set; }

        public virtual List<TokenProperty> Properties { get; set; }
    }
}