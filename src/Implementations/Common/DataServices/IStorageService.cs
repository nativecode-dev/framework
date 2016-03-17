namespace Common.DataServices
{
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Data.Entities;

    public interface IStorageService
    {
        Task<Storage> GetByNameAsync(string name, CancellationToken cancellationToken);
    }
}