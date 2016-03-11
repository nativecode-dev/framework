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
        Task<bool> BeginWorkAsync(Guid key, CancellationToken cancellationToken);

        Task<bool> CompleteWorkAsync(Guid key, CancellationToken cancellationToken);

        Task<bool> FailWorkAsync(Guid key, CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> GetWorkAsync(int count, CancellationToken cancellationToken);
    }
}