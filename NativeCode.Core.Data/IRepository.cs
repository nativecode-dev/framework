namespace NativeCode.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides a contract to manage entities
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <seealso cref="System.IDisposable" />
    public interface IRepository<TEntity, out TContext> : IDisposable
        where TEntity : class, IEntity where TContext : class, IDataContext
    {
        /// <summary>
        /// Gets the data context.
        /// </summary>
        TContext DataContext { get; }

        /// <summary>
        /// Finds a single matching entity.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>Returns a matching entity.</returns>
        TEntity Find<TKey>(TKey key) where TKey : struct;

        /// <summary>
        /// Finds a single matching entity.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a matching entity.</returns>
        Task<TEntity> FindAsync<TKey>(TKey key, CancellationToken cancellationToken) where TKey : struct;

        /// <summary>
        /// Allows creating a custom query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>Returns a modified <see cref="IQueryable" />.</returns>
        IQueryable<TEntity> Query(Func<IQueryable<TEntity>, IQueryable<TEntity>> query);

        /// <summary>
        /// Allows creating a custom query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>Returns a modified  <see cref="IQueryable" />.</returns>
        Task<IQueryable<TEntity>> QueryAsync(Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> query);

        /// <summary>
        /// Saves the provided entity
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if save succeeded, <c>false</c> otherwise.</returns>
        bool Save(TEntity entity);

        /// <summary>
        /// Saves the provided entities
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns><c>true</c> if save succeeded, <c>false</c> otherwise.</returns>
        bool Save(IEnumerable<TEntity> entities);

        /// <summary>
        /// Saves the provided entity
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><c>true</c> if save succeeded, <c>false</c> otherwise.</returns>
        Task<bool> SaveAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Saves the provided entities
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><c>true</c> if save succeeded, <c>false</c> otherwise.</returns>
        Task<bool> SaveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    }
}