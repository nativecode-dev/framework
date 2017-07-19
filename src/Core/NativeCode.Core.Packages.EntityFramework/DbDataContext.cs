namespace NativeCode.Core.Packages.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Data;
    using Platform;
    using Platform.Connections;

    [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification =
        "Disposable implemented in the base class.")]
    public abstract class DbDataContext : DbContext, IDataContext
    {
        private readonly List<Action<DbEntityEntry>> interceptors = new List<Action<DbEntityEntry>>();

        private readonly IPlatform platform;

        protected DbDataContext(IConnectionStringProvider provider, IPlatform platform)
            : this(provider.GetDefaultConnectionString().ToString(), platform)
        {
        }

        protected DbDataContext(string connectionString, IPlatform platform)
            : base(connectionString)
        {
            this.interceptors.Add(this.UpdateAuditProperties);
            this.interceptors.Add(UpdateKeyProperties);
            this.platform = platform;
        }

        public override int SaveChanges()
        {
            this.ProcessInterceptors();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            this.ProcessInterceptors();

            return base.SaveChangesAsync(cancellationToken);
        }

        protected void SetEntity<T>(T entity) where T : class, IEntity
        {
            if (this.ChangeTracker.Entries().Any(e => e.Entity == entity) == false)
                this.Set<T>().Add(entity);
        }

        private static void UpdateKeyProperties(DbEntityEntry entry)
        {
            var entity = entry.Entity as IEntity<Guid>;

            if (entity != null && entry.State == EntityState.Added)
                entity.Id = Guid.NewGuid();
        }

        private void ProcessInterceptors()
        {
            foreach (var entry in this.ChangeTracker.Entries())
            {
                foreach (var interceptor in this.interceptors)
                    interceptor(entry);
            }
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
    }
}