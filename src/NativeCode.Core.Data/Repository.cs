namespace NativeCode.Core.Data
{
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Types;

    public class Repository<TEntity, TContext> : Disposable, IRepository<TEntity, TContext>
        where TEntity : class, IEntity where TContext : class, IDataContext
    {
        protected Repository(TContext context)
        {
            this.DataContext = context;
        }

        public TContext DataContext { get; private set; }

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

        public Task<TEntity> FindAsync<TKey>(TKey key, CancellationToken cancellationToken) where TKey : struct
        {
            return this.DataContext.FindAsync<TEntity, TKey>(key, cancellationToken);
        }
    }
}