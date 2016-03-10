namespace NativeCode.Packages.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Data;
    using NativeCode.Core.Providers;

    public abstract class DbDataContext : DbContext, IDataContext
    {
        protected DbDataContext(IConnectionStringProvider provider)
            : base(provider.GetDefaultConnectionString().ToString())
        {
        }

        public virtual async Task<T> FindAsync<T, TKey>(TKey key, CancellationToken cancellationToken) where T : class, IEntity where TKey : struct
        {
            var dbset = this.Set(typeof(T));
            var record = await dbset.FindAsync(cancellationToken, key);

            return (T)record;
        }

        public bool Save()
        {
            return this.SaveChanges() > 0;
        }

        public bool Save<T>(T entity) where T : class, IEntity
        {
            this.SetEntity(entity);

            return this.Save();
        }

        public bool Save<T>(IEnumerable<T> entities) where T : class, IEntity
        {
            foreach (var entity in entities)
            {
                this.SetEntity(entity);
            }

            return this.Save();
        }

        public async Task<bool> SaveAsync(CancellationToken cancellationToken)
        {
            return await this.SaveChangesAsync(cancellationToken) > 0;
        }

        public Task<bool> SaveAsync<T>(T entity, CancellationToken cancellationToken) where T : class, IEntity
        {
            this.SetEntity(entity);

            return this.SaveAsync(cancellationToken);
        }

        public Task<bool> SaveAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken) where T : class, IEntity
        {
            foreach (var entity in entities)
            {
                this.SetEntity(entity);
            }

            return this.SaveAsync(cancellationToken);
        }

        protected void SetEntity<T>(T entity) where T : class, IEntity
        {
            var dbset = this.Set<T>();
            dbset.Add(entity);
        }
    }
}