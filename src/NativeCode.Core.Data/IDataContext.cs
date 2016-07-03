namespace NativeCode.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDataContext : IDisposable
    {
        Task<T> FindAsync<T, TKey>(TKey key, CancellationToken cancellationToken) where T : class, IEntity where TKey : struct;

        bool Save();

        bool Save<T>(T entity) where T : class, IEntity;

        bool Save<T>(IEnumerable<T> entities) where T : class, IEntity;

        Task<bool> SaveAsync(CancellationToken cancellationToken);

        Task<bool> SaveAsync<T>(T entity, CancellationToken cancellationToken) where T : class, IEntity;

        Task<bool> SaveAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken) where T : class, IEntity;
    }
}
