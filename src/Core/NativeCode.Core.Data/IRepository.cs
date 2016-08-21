namespace NativeCode.Core.Data
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides a contract to manage entities
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="System.IDisposable" />
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class, IEntity
    {
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
        /// <typeparam name="T">The entity type.</typeparam>
        /// <param name="query">The query.</param>
        /// <returns>Returns a modified <see cref="IQueryable" />.</returns>
        IQueryable<T> Query<T>(Func<IQueryable<TEntity>, IQueryable<T>> query);
    }
}