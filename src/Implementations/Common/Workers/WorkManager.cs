namespace Common.Workers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core;
    using NativeCode.Core.Data;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Logging;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Types;

    public abstract class WorkManager<TEntity> : Disposable, IWorkManager<TEntity>
        where TEntity : class, IEntity
    {
        private const string GlobalKeyMaxConcurrency = "global.settings.work_manager.max_concurrency";

        private readonly IApplication application;

        protected WorkManager(IApplication application, ILogger logger, IWorkProvider<TEntity> work)
        {
            this.application = application;

            this.Logger = logger;
            this.WorkProvider = work;
        }

        protected CancellationTokenSource CancellationTokenSource { get; private set; }

        protected Task Task { get; private set; }

        protected ILogger Logger { get; }

        protected IWorkProvider<TEntity> WorkProvider { get; private set; }

        public int MaxConcurrency { get; set; } = Global.Settings.GetValue(GlobalKeyMaxConcurrency, 10);

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

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.Disposed)
            {
                if (this.CancellationTokenSource != null)
                {
                    this.CancellationTokenSource.Dispose();
                    this.CancellationTokenSource = null;
                }

                if (this.WorkProvider != null)
                {
                    this.WorkProvider.Dispose();
                    this.WorkProvider = null;
                }
            }

            base.Dispose(disposing);
        }

        protected async Task RunLoopAsync()
        {
            using (var root = this.application.Platform.CreateDependencyScope())
            {
                var tasks = new List<TaskMapping>();
                var token = this.CancellationTokenSource.Token;
                var resumables = (await this.WorkProvider.GetResumableWorkAsync(token).ConfigureAwait(false)).ToList();

                do
                {
                    try
                    {
                        using (root.CreateChildContainer())
                        {
                            // If we have reached max concurrency, we'll wait until another slot opens up.
                            if (tasks.Count == this.MaxConcurrency)
                            {
                                await this.WaitForAvailableTaskAsync(tasks, token).ConfigureAwait(false);
                            }

                            // Check for any work left from last run.
                            if (await this.EnqueueResumableTaskAsync(resumables, tasks, token).ConfigureAwait(false))
                            {
                                continue;
                            }

                            var retryables = (await this.WorkProvider.GetRetryableWorkAsync(token).ConfigureAwait(false)).ToList();

                            if (await this.EnqueueRetryableTaskAsync(retryables, tasks, token).ConfigureAwait(false))
                            {
                                continue;
                            }

                            // Enqueue waiting work.
                            if (!await this.EnqueueQueuedTasksAsync(tasks, token).ConfigureAwait(false))
                            {
                                await this.RemoveCompletedTasksAsync(tasks, token).ConfigureAwait(false);
                            }
                        }

                        // Tiny cooldown so we don't eat up 100% of the CPU in a non-resting loop.
                        await Task.Delay(TimeSpan.FromSeconds(1), token).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        this.Logger.Exception(ex);
                    }
                }
                while (!this.CancellationTokenSource.IsCancellationRequested);
            }
        }

        protected abstract Task ExecuteWorkAsync(TEntity entity, CancellationToken cancellationToken);

        protected abstract Task ResumeWorkAsync(TEntity entity, CancellationToken cancellationToken);

        protected abstract Task RetryWorkAsync(TEntity entity, CancellationToken cancellationToken);

        private Task RemoveCompletedTasksAsync(ICollection<TaskMapping> mappings, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();

            foreach (var mapping in mappings.Where(x => x.Task.IsDone()).ToList())
            {
                mappings.Remove(mapping);
                tasks.Add(this.UpdateMappingStateAsync(mapping, cancellationToken));
            }

            if (tasks.Any())
            {
                this.Logger.Debug($"Removing {tasks.Count} completed tasks.");

                return Task.WhenAll(tasks);
            }

            return Task.FromResult(0);
        }

        private async Task<bool> EnqueueResumableTaskAsync(ICollection<TEntity> resumables, ICollection<TaskMapping> tasks, CancellationToken cancellationToken)
        {
            var entity = resumables.FirstOrDefault();

            if (entity != null && await this.WorkProvider.BeginWorkAsync(entity, cancellationToken).ConfigureAwait(false))
            {
                var task = Task.Run(() => this.ResumeWorkAsync(entity, cancellationToken), cancellationToken);
                var mapping = new TaskMapping(entity, task);
                tasks.Add(mapping);
                resumables.Remove(entity);

                return true;
            }

            return false;
        }

        private async Task<bool> EnqueueRetryableTaskAsync(ICollection<TEntity> retryables, ICollection<TaskMapping> tasks, CancellationToken cancellationToken)
        {
            var entity = retryables.FirstOrDefault();

            if (entity != null && await this.WorkProvider.BeginWorkAsync(entity, cancellationToken).ConfigureAwait(false))
            {
                var task = Task.Run(() => this.RetryWorkAsync(entity, cancellationToken), cancellationToken);
                var mapping = new TaskMapping(entity, task);
                tasks.Add(mapping);
                retryables.Remove(entity);

                return true;
            }

            return false;
        }

        private async Task<bool> EnqueueQueuedTasksAsync(ICollection<TaskMapping> tasks, CancellationToken cancellationToken)
        {
            var count = this.MaxConcurrency - tasks.Count;
            var entities = (await this.WorkProvider.GetWorkAsync(count, cancellationToken).ConfigureAwait(false)).ToList();

            if (entities.Any())
            {
                foreach (var entity in entities)
                {
                    if (await this.WorkProvider.BeginWorkAsync(entity, cancellationToken).ConfigureAwait(false))
                    {
                        var task = Task.Run(() => this.ExecuteWorkAsync(entity, cancellationToken), cancellationToken);
                        var mapping = new TaskMapping(entity, task);
                        tasks.Add(mapping);
                    }
                }

                this.Logger.Debug($"Found {tasks.Count} units of work to perform. Creating tasks...");

                return true;
            }

            return false;
        }

        private async Task WaitForAvailableTaskAsync(ICollection<TaskMapping> tasks, CancellationToken cancellationToken)
        {
            this.Logger.Debug($"Max concurrency {this.MaxConcurrency} reached, waiting for a task to complete...");

            var completed = await Task.WhenAny(tasks.Select(x => x.Task)).ConfigureAwait(false);

            if (completed != null)
            {
                var mapping = tasks.Single(x => x.Task == completed);

                if (await this.UpdateMappingStateAsync(mapping, cancellationToken).ConfigureAwait(false))
                {
                    tasks.Remove(mapping);
                }
            }
        }

        private Task<bool> UpdateMappingStateAsync(TaskMapping mapping, CancellationToken cancellationToken)
        {
            if (mapping.Task.IsErrorState())
            {
                return this.WorkProvider.FailWorkAsync(mapping.Entity, cancellationToken);
            }

            return this.WorkProvider.CompleteWorkAsync(mapping.Entity, cancellationToken);
        }

        private class TaskMapping
        {
            public TaskMapping(TEntity entity, Task task)
            {
                this.Entity = entity;
                this.Task = task;
            }

            public TEntity Entity { get; }

            public Task Task { get; }
        }
    }
}