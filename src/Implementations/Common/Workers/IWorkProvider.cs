namespace Common.Workers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Data;

    public interface IWorkProvider<TEntity> : IDisposable
        where TEntity : class, IEntity
    {
        Task<bool> BeginWorkAsync(TEntity entity, CancellationToken cancellationToken);

        Task<bool> CompleteWorkAsync(TEntity entity, CancellationToken cancellationToken);

        Task<bool> FailWorkAsync(TEntity entity, CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> GetResumableWorkAsync(CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> GetRetryableWorkAsync(CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> GetWorkAsync(int count, CancellationToken cancellationToken);
    }
}