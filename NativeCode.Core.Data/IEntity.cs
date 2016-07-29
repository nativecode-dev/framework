namespace NativeCode.Core.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public interface IEntity
    {
        DateTimeOffset? DateCreated { get; }

        DateTimeOffset? DateModified { get; }

        string UserCreated { get; }

        string UserModified { get; }
    }

    public interface IEntity<out TKey> : IEntity
        where TKey : struct
    {
        [Key]
        TKey Id { get; }
    }
}