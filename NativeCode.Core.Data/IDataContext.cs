namespace NativeCode.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides a contract for providing a data context.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IDataContext : IDisposable
    {
        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <typeparam name="T">The entity type.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        bool Save<T>(T entity) where T : class, IEntity;

        /// <summary>
        /// Saves the specified entities.
        /// </summary>
        /// <typeparam name="T">The entity type.</typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        bool Save<T>(IEnumerable<T> entities) where T : class, IEntity;

        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <typeparam name="T">The entity type.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        Task<bool> SaveAsync<T>(T entity, CancellationToken cancellationToken) where T : class, IEntity;

        /// <summary>
        /// Saves the specified entities.
        /// </summary>
        /// <typeparam name="T">The entity type.</typeparam>
        /// <param name="entities">The entities.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        Task<bool> SaveAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken) where T : class, IEntity;
    }
}
