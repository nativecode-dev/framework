namespace NativeCode.Core.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
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

    public abstract class Entity<TKey> : Entity, IEntityKeySetter<TKey>
        where TKey : struct
    {
        [Key]
        public TKey Key { get; protected set; }

        public void SetKey(TKey key)
        {
            this.Key = key;
        }
    }
}
