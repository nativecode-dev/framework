namespace NativeCode.Core.Data
{
    using System;
    using System.Security.Principal;
    using JetBrains.Annotations;

    /// <summary>
    /// Provides a contract to modify audit properties.
    /// </summary>
    /// <seealso cref="NativeCode.Core.Data.IEntity" />
    public interface IEntityAuditor : IEntity
    {
        /// <summary>
        /// Sets the date created.
        /// </summary>
        /// <param name="value">The value.</param>
        void SetDateCreated(DateTimeOffset value);

        /// <summary>
        /// Sets the date modified.
        /// </summary>
        /// <param name="value">The value.</param>
        void SetDateModified(DateTimeOffset value);

        /// <summary>
        /// Sets the user created.
        /// </summary>
        /// <param name="identity">The identity.</param>
        void SetUserCreated([NotNull] IIdentity identity);

        /// <summary>
        /// Sets the user modified.
        /// </summary>
        /// <param name="identity">The identity.</param>
        void SetUserModified([NotNull] IIdentity identity);
    }
}