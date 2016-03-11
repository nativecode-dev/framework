namespace Common.Workers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Data;
    using NativeCode.Core.Types;

    public abstract class WorkManager<TEntity> : Disposable, IWorkManager<TEntity>
        where TEntity : class, IEntity
    {
        protected CancellationTokenSource CancellationTokenSource { get; private set; }

        protected Task Task { get; private set; }

        public Task StartAsync()
        {
            if (this.CancellationTokenSource == null)
            {
                this.CancellationTokenSource = new CancellationTokenSource();
                return this.Task = Task.Run(this.RunLoopAsync, this.CancellationTokenSource.Token);
            }

            return this.Task;
        }

        public void Stop()
        {
            this.CancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(2));
        }

        protected async Task RunLoopAsync()
        {
            do
            {
                await Task.Delay(TimeSpan.FromMilliseconds(10));
            }
            while (!this.CancellationTokenSource.IsCancellationRequested);
        }
    }
}