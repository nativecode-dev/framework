namespace Common.Workers
{
    using System;
    using System.Threading.Tasks;

    using NativeCode.Core.Data;

    public interface IWorkManager<TEntity> : IDisposable
        where TEntity : class, IEntity
    {
        Task StartAsync();

        void Stop();
    }
}