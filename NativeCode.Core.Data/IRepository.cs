namespace NativeCode.Core.Data
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IRepository<TEntity, out TContext> : IDisposable
        where TEntity : class, IEntity where TContext : class, IDataContext
    {
        TContext DataContext { get; }

        Task<TEntity> FindAsync<TKey>(TKey key, CancellationToken cancellationToken) where TKey : struct;
    }
}