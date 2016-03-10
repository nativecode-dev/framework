namespace NativeCode.Core.Data
{
    using System;
    using System.Security.Principal;

    using JetBrains.Annotations;

    internal interface IEntityAuditor
    {
        void SetDateCreated(DateTimeOffset value);

        void SetDateModified(DateTimeOffset value);

        void SetUserCreated([NotNull] IIdentity identity);

        void SetUserModified([NotNull] IIdentity identity);
    }
}