namespace NativeCode.Core.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using System.Security.Principal;

    public abstract class Entity : IEntityAuditor
    {
        public DateTimeOffset? DateCreated { get; protected set; }

        public DateTimeOffset? DateModified { get; protected set; }

        public string UserCreated { get; protected set; }

        public string UserModified { get; protected set; }

        public void SetDateCreated(DateTimeOffset value)
        {
            this.DateCreated = value;
        }

        public void SetDateModified(DateTimeOffset value)
        {
            this.DateModified = value;
        }

        public void SetUserCreated(IIdentity identity)
        {
            this.UserCreated = identity.Name;
        }

        public void SetUserModified(IIdentity identity)
        {
            this.UserModified = identity.Name;
        }
    }

    [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Generic type.")]
    public abstract class Entity<T> : Entity
        where T : struct
    {
        [Key]
        public T Id { get; set; }

        public void SetId(T id)
        {
            this.Id = id;
        }
    }
}