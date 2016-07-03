namespace NativeCode.Packages.Data
{
    using NativeCode.Core.Data;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Platform.Connections;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Threading;
    using System.Threading.Tasks;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
    public abstract class DbDataContext : DbContext, IDataContext
    {
        private readonly List<Action<DbEntityEntry>> interceptors = new List<Action<DbEntityEntry>>();

        private readonly IPlatform platform;

        protected DbDataContext(IConnectionStringProvider provider, IPlatform platform)
            : base(provider.GetDefaultConnectionString().ToString())
        {
            this.interceptors.Add(this.UpdateAuditProperties);
            this.interceptors.Add(this.UpdateKeyProperties);
            this.platform = platform;
        }

        private void UpdateAuditProperties(DbEntityEntry entry)
        {
            var auditor = entry.Entity as IEntityAuditor;

            if (auditor != null)
            {
                var principal = this.platform.GetCurrentPrincipal();

                if (entry.State == EntityState.Added)
                {
                    auditor.SetDateCreated(DateTimeOffset.UtcNow);
                    auditor.SetUserCreated(principal.Identity);
                }

                auditor.SetDateModified(DateTimeOffset.UtcNow);
                auditor.SetUserModified(principal.Identity);
            }
        }

        private void UpdateKeyProperties(DbEntityEntry entry)
        {
            var setter = entry.Entity as IEntityKeySetter<Guid>;

            if (setter != null && entry.State == EntityState.Added)
            {
                setter.SetKey(Guid.NewGuid());
            }
        }

        public virtual async Task<T> FindAsync<T, TKey>(TKey key, CancellationToken cancellationToken) where T : class, IEntity where TKey : struct
        {
            var dbset = this.Set(typeof(T));
            var record = await dbset.FindAsync(cancellationToken, key).ConfigureAwait(false);

            return (T)record;
        }

        public bool Save()
        {
            this.ProcessEntityChanges();

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
            this.ProcessEntityChanges();
            var changed = await this.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return changed > 0;
        }

        public async Task<bool> SaveAsync<T>(T entity, CancellationToken cancellationToken) where T : class, IEntity
        {
            this.SetEntity(entity);

            return await this.SaveAsync(cancellationToken);
        }

        public async Task<bool> SaveAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken) where T : class, IEntity
        {
            foreach (var entity in entities)
            {
                this.SetEntity(entity);
            }

            return await this.SaveAsync(cancellationToken);
        }

        protected void SetEntity<T>(T entity) where T : class, IEntity
        {
            var dbset = this.Set<T>();
            dbset.Add(entity);
        }

        private void ProcessEntityChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries())
            {
                foreach (var interceptor in this.interceptors)
                {
                    interceptor(entry);
                }
            }
        }
    }
}
