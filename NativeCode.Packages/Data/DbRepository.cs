namespace NativeCode.Packages.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Data;
    using NativeCode.Core.Types;

    /// <summary>
    /// Provides a base class to manage entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the t entity.</typeparam>
    /// <typeparam name="TContext">The type of the t context.</typeparam>
    /// <seealso cref="NativeCode.Core.Types.Disposable" />
    /// <seealso cref="NativeCode.Core.Data.IRepository{TEntity, TContext}" />
    public abstract class DbRepository<TEntity, TContext> : Disposable, IRepository<TEntity, TContext>
        where TEntity : class, IEntity where TContext : DbContext, IDataContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbRepository{TEntity,TContext}" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected DbRepository(TContext context)
        {
            this.DataContext = context;
        }

        public TContext DataContext { get; private set; }

        public virtual TEntity Find<TKey>(TKey key) where TKey : struct
        {
            return this.DataContext.Set<TEntity>().Find(key);
        }

        public virtual Task<TEntity> FindAsync<TKey>(TKey key, CancellationToken cancellationToken) where TKey : struct
        {
            return this.DataContext.Set<TEntity>().FindAsync(cancellationToken, key);
        }

        public virtual IQueryable<TEntity> Query(Func<IQueryable<TEntity>, IQueryable<TEntity>> query)
        {
            return query(this.DataContext.Set<TEntity>());
        }

        public IQueryable<T> Query<T>(Func<IQueryable<TEntity>, IQueryable<T>> query)
        {
            return query(this.DataContext.Set<TEntity>());
        }

        public virtual Task<IQueryable<TEntity>> QueryAsync(Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> query)
        {
            return query(this.DataContext.Set<TEntity>());
        }

        public Task<IQueryable<T>> QueryAsync<T>(Func<IQueryable<TEntity>, Task<IQueryable<T>>> query)
        {
            return query(this.DataContext.Set<TEntity>());
        }

        public virtual bool Save(TEntity entity)
        {
            return this.DataContext.Save(entity);
        }

        public virtual bool Save(IEnumerable<TEntity> entities)
        {
            return this.DataContext.Save(entities);
        }

        public virtual Task<bool> SaveAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return this.DataContext.SaveAsync(entity, cancellationToken);
        }

        public virtual Task<bool> SaveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            return this.DataContext.SaveAsync(entities, cancellationToken);
        }

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