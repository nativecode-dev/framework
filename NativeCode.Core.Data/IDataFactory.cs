namespace NativeCode.Core.Data
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides a contract to manage seed data.
    /// </summary>
    public interface IDataFactory
    {
        /// <summary>
        /// Creates the specified key.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="setter">The setter.</param>
        /// <returns>Returns a new entity.</returns>
        TEntity Create<TEntity, TKey>(TKey key, Action<TEntity> setter = null) where TEntity : class, IEntity<TKey>, new() where TKey : struct;

        /// <summary>
        /// Seeds the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Seed<TEntity>(TEntity entity) where TEntity : class, IEntity;

        /// <summary>
        /// Seeds the specified entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entities">The entities.</param>
        void Seed<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity;
    }
}