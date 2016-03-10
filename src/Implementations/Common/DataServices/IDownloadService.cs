namespace Common.DataServices
{
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Data.Entities;

    public interface IDownloadService
    {
        Task<Download> QueueAsync(string path, string filename, string url, CancellationToken cancellationToken);
    }
}