namespace Common.Data.Entities
{
    using System;

    using NativeCode.Core.Data;

    public class Token : Entity<Guid>
    {
        public Account Account { get; set; }

        public DateTimeOffset? ExpirationDate { get; set; }
    }
}