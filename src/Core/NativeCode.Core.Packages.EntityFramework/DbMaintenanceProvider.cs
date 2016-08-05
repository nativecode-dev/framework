namespace NativeCode.Core.Packages.EntityFramework
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using NativeCode.Core.Platform.Connections;
    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Platform.Maintenance;
    using NativeCode.Core.Types;

    public class DbMaintenanceProvider<T> : IMaintenanceProvider
        where T : DbDataContext
    {
        private readonly LazyFactory<bool> migrations;

        public DbMaintenanceProvider(IConnectionStringProvider connections, ILogger logger)
        {
            this.migrations = new LazyFactory<bool>(() => this.PendingMigrations(connections.GetConnectionString<T>()));
            this.Logger = logger;
        }

        public bool Active => this.migrations.Value;

        public string Name => $"DBCONTEXT:{typeof(T).FullName}";

        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the maintenance key.
        /// </summary>
        protected string MaintenanceKey { get; private set; }

        public string EnterMaintenance()
        {
            if (string.IsNullOrWhiteSpace(this.MaintenanceKey))
            {
                var key = this.GenerateKey();
                this.Logger.Informational($"Entering maintenance mode. Use {key} to unlock.");
            }

            return this.MaintenanceKey;
        }

        public void ExitMaintenance(string key)
        {
            if (string.IsNullOrWhiteSpace(this.MaintenanceKey) == false && this.MaintenanceKey == key)
            {
                this.MaintenanceKey = null;
                this.migrations.Reset();
            }
        }

        protected virtual string GenerateKey()
        {
            return this.MaintenanceKey = Guid.NewGuid().ToString();
        }

        protected bool PendingMigrations(string connectionString, string providerName = "System.Data.SqlClient")
        {
            var name = typeof(T).Name;
            this.Logger.Debug($"Checking pending migrations for {name}.");
            this.Logger.Informational($"Provider='{providerName}', ConnectionString='{connectionString}'");

            try
            {
                var configuration = new DbMigrationsConfiguration<T> { TargetDatabase = new DbConnectionInfo(connectionString, providerName) };
                var migration = new DbMigrator(configuration);
                var pending = migration.GetPendingMigrations().Any();

                if (pending)
                {
                    this.EnterMaintenance();
                }

                return pending;
            }
            catch (Exception ex)
            {
                this.Logger.Exception(ex);
            }

            this.Logger.Debug($"No pending migrations found for {name}.");

            return false;
        }
    }
}
