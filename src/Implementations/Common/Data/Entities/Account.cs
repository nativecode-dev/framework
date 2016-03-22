namespace Common.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using NativeCode.Core.Data;
    using NativeCode.Core.Validation;

    public class Account : Entity<long>
    {
        [MaxLength(128)]
        public string DomainHost { get; set; }

        [MaxLength(128)]
        [Index("UX_Account", IsUnique = true, Order = 0)]
        public string DomainName { get; set; }

        public List<Download> Downloads { get; set; } = new List<Download>(200);

        [Index("UX_Account", IsUnique = true, Order = 1)]
        [MaxLength(256)]
        public string Login { get; set; }

        [MaxLength(32)]
        [StringComplexity(StringComplexityRules.RequireAll)]
        public string Password { get; set; }

        public List<AccountProperty> Properties { get; set; } = new List<AccountProperty>(20);

        public AccountSource Source { get; set; }
    }
}