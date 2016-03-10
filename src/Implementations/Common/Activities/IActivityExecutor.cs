namespace Common.Activities
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Data.Entities;

    using NativeCode.Core.Platform;
    using NativeCode.Core.Types;

    public interface IActivityExecutor<in T> : IDisposable
        where T : class
    {
        Task ExecuteAsync(T instance, CancellationToken cancellationToken);
    }

    public class DownloadActivityExecutor : Disposable, IActivityExecutor<Download>
    {
        private readonly HttpClient client = new HttpClient(CreateMessageHandler(), true);

        public DownloadActivityExecutor(IApplication application)
        {
            
        }

        public async Task ExecuteAsync(Download instance, CancellationToken cancellationToken)
        {
            using (var stream = await this.client.GetStreamAsync(instance.Url))
            {

            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.Disposed)
            {
                this.client.Dispose();
            }

            base.Dispose(disposing);
        }

        private static HttpMessageHandler CreateMessageHandler()
        {
            return new HttpClientHandler();
        }
    }
}