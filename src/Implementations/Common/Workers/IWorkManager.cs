namespace Common.Workers
{
    using System;
    using System.Threading.Tasks;

    using NativeCode.Core.Data;

    public interface IWorkManager<TEntity> : IDisposable
        where TEntity : class, IEntity
    {
        int MaxConcurrency { get; set; }

        Task StartAsync();

        void Stop();
    }
}