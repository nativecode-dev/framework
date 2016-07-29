namespace NativeCode.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDataContext : IDisposable
    {
        bool Save<T>(T entity) where T : class, IEntity;

        bool Save<T>(IEnumerable<T> entities) where T : class, IEntity;

        Task<bool> SaveAsync<T>(T entity, CancellationToken cancellationToken) where T : class, IEntity;

        Task<bool> SaveAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken) where T : class, IEntity;
    }
}
