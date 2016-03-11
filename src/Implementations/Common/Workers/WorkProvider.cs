namespace Common.Workers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Data;
    using NativeCode.Core.Types;

    public abstract class WorkProvider<TEntity> : Disposable, IWorkProvider<TEntity>
        where TEntity : class, IEntity
    {
        public abstract Task<bool> BeginWorkAsync(Guid key, CancellationToken cancellationToken);

        public abstract Task<bool> CompleteWorkAsync(Guid key, CancellationToken cancellationToken);

        public abstract Task<bool> FailWorkAsync(Guid key, CancellationToken cancellationToken);

        public abstract Task<IEnumerable<TEntity>> GetWorkAsync(int count, CancellationToken cancellationToken);
    }
}