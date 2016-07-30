namespace NativeCode.Core.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Provides a contract to define standard entity properties.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets the date created.
        /// </summary>
        [DataType(DataType.DateTime)]
        DateTimeOffset? DateCreated { get; }

        /// <summary>
        /// Gets the date modified.
        /// </summary>
        [DataType(DataType.DateTime)]
        DateTimeOffset? DateModified { get; }

        /// <summary>
        /// Gets the user created.
        /// </summary>
        [StringLength(64)]
        string UserCreated { get; }

        /// <summary>
        /// Gets the user modified.
        /// </summary>
        [StringLength(64)]
        string UserModified { get; }
    }

    /// <summary>
    /// Provides a contract to define standard entity properties.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <seealso cref="NativeCode.Core.Data.IEntity" />
    public interface IEntity<out TKey> : IEntity
        where TKey : struct
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        [Key]
        TKey Id { get; }
    }
}