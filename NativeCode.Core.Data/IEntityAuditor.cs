namespace NativeCode.Core.Data
{
    using System;
    using System.Security.Principal;

    using JetBrains.Annotations;

    public interface IEntityAuditor : IEntity
    {
        void SetDateCreated(DateTimeOffset value);

        void SetDateModified(DateTimeOffset value);

        void SetUserCreated([NotNull] IIdentity identity);

        void SetUserModified([NotNull] IIdentity identity);
    }
}