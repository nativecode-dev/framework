namespace NativeCode.Core.Packages.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Data;
    using Types;

    /// <summary>
    /// Provides a base class to manage entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the t entity.</typeparam>
    /// <typeparam name="TContext">The type of the t context.</typeparam>
    /// <seealso cref="NativeCode.Core.Data.IRepository{TEntity}" />
    /// <seealso cref="NativeCode.Core.Data.IRepositoryContext{TContext}" />
    /// <seealso cref="NativeCode.Core.Types.DisposableManager" />
    public class DbRepository<TEntity, TContext> : DisposableManager, IRepository<TEntity>, IRepositoryContext<TContext>
        where TEntity : class, IEntity where TContext : DbContext, IDataContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbRepository{TEntity,TContext}" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DbRepository(TContext context)
        {
            this.DataContext = context;
            this.EnsureDisposed(this.DataContext);
        }

        public TContext DataContext { get; }

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
    }
}