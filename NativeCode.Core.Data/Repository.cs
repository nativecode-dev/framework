namespace NativeCode.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Types;

    /// <summary>
    /// Provides a base class to manage entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the t entity.</typeparam>
    /// <typeparam name="TContext">The type of the t context.</typeparam>
    /// <seealso cref="NativeCode.Core.Types.Disposable" />
    /// <seealso cref="NativeCode.Core.Data.IRepository{TEntity, TContext}" />
    public abstract class Repository<TEntity, TContext> : Disposable, IRepository<TEntity, TContext>
        where TEntity : class, IEntity where TContext : class, IDataContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity, TContext}" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected Repository(TContext context)
        {
            this.DataContext = context;
        }

        public TContext DataContext { get; private set; }

        public abstract TEntity Find<TKey>(TKey key) where TKey : struct;

        public abstract Task<TEntity> FindAsync<TKey>(TKey key, CancellationToken cancellationToken) where TKey : struct;

        public abstract IQueryable<TEntity> Query(Func<IQueryable<TEntity>, IQueryable<TEntity>> query);

        public abstract Task<IQueryable<TEntity>> QueryAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> query);

        public abstract bool Save(TEntity entity);

        public abstract bool Save(IEnumerable<TEntity> entities);

        public abstract Task<bool> SaveAsync(TEntity entity);

        public abstract Task<bool> SaveAsync(IEnumerable<TEntity> entities);

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.Disposed)
            {
                this.DataContext.Dispose();

                if (this.DataContext != null)
                {
                    this.DataContext.Dispose();
                    this.DataContext = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}