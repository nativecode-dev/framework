namespace Common.DataServices
{
    using System.Transactions;

    using Common.Data;

    using NativeCode.Core.Data;
    using NativeCode.Core.Types;

    public abstract class DataService<TEntity> : Disposable, IDataService<TEntity>
        where TEntity : class, IEntity
    {
        protected DataService(IRepository<TEntity, CoreDataContext> repository)
        {
            this.Repository = repository;
        }

        protected CoreDataContext Context => this.Repository.DataContext;

        protected IRepository<TEntity, CoreDataContext> Repository { get; private set; }

        protected TransactionScope CreateTransactionScope()
        {
            return new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.Disposed)
            {
                if (this.Repository != null)
                {
                    this.Repository.Dispose();
                    this.Repository = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}