namespace NativeCode.Core.Data
{
    /// <summary>
    /// Provides a contract to edit the entity key.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <seealso cref="NativeCode.Core.Data.IEntity{TKey}" />
    public interface IEntityIdSetter<TKey> : IEntity<TKey>
        where TKey : struct
    {
        /// <summary>
        /// Sets the identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void SetId(TKey id);
    }
}