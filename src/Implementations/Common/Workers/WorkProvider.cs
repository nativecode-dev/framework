namespace Common.Workers
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Data;
    using NativeCode.Core.Types;

    public abstract class WorkProvider<TEntity> : Disposable, IWorkProvider<TEntity>
        where TEntity : class, IEntity
    {
        public abstract Task<bool> BeginWorkAsync(TEntity entity, CancellationToken cancellationToken);

        public abstract Task<bool> CompleteWorkAsync(TEntity entity, CancellationToken cancellationToken);

        public abstract Task<bool> FailWorkAsync(TEntity entity, CancellationToken cancellationToken);

        public abstract Task<IEnumerable<TEntity>> GetResumableWorkAsync(CancellationToken cancellationToken);

        public abstract Task<IEnumerable<TEntity>> GetRetryableWorkAsync(CancellationToken cancellationToken);

        public abstract Task<IEnumerable<TEntity>> GetWorkAsync(int count, CancellationToken cancellationToken);
    }
}