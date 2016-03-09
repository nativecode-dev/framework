namespace NativeCode.Core.Data
{
    using System;

    public interface IEntity
    {
        DateTimeOffset? DateCreated { get; }

        DateTimeOffset? DateModified { get; }

        string UserCreated { get; }

        string UserModified { get; }
    }
}