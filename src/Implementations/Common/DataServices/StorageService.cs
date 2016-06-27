namespace Common.DataServices
{
    using System.Data.Entity;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Data;
    using Common.Data.Entities;
    using Common.Data.Entities.Storage;

    using NativeCode.Core.Data;

    public class StorageService : DataService<Storage>, IStorageService
    {
        public StorageService(IRepository<Storage, CoreDataContext> repository)
            : base(repository)
        {
        }

        public Task<Storage> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return this.Context.Storage.SingleOrDefaultAsync(x => x.Name == name, cancellationToken);
        }
    }
}