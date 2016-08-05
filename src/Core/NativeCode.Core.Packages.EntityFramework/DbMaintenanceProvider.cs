namespace NativeCode.Core.Packages.EntityFramework
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using NativeCode.Core.Platform.Connections;
    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Platform.Maintenance;

    public class DbMaintenanceProvider<T> : IMaintenanceProvider
        where T : DbDataContext
    {
        public DbMaintenanceProvider(IConnectionStringProvider connections, ILogger logger)
        {
            this.Logger = logger;

            if (this.HasPendingMigrations(connections.GetDefaultConnectionString()))
            {
                this.EnterMaintenance();
            }
        }

        public bool Active { get; private set; }

        public string Name => $"DbContext: {typeof(T).FullName}";

        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the maintenance key.
        /// </summary>
        protected string MaintenanceKey { get; private set; }

        public string EnterMaintenance()
        {
            this.Active = true;
            this.GenerateMaintenanceKey();
            this.Logger.Informational($"Entering maintenance mode. Use {this.MaintenanceKey} to unlock.");

            return this.MaintenanceKey;
        }

        public void ExitMaintenance(string key)
        {
            this.Active = key == this.MaintenanceKey;
            this.MaintenanceKey = null;
        }

        protected virtual string GenerateMaintenanceKey()
        {
            this.MaintenanceKey = Guid.NewGuid().ToString();
            return this.MaintenanceKey;
        }

        protected bool HasPendingMigrations(string connectionString, string providerName = "System.Data.SqlClient")
        {
            this.Logger.Debug($"Checking for pending migrations using [{connectionString}] with [{providerName}].");

            try
            {
                var configuration = new DbMigrationsConfiguration<T> { TargetDatabase = new DbConnectionInfo(connectionString, providerName) };
                var migration = new DbMigrator(configuration);

                var pending = migration.GetPendingMigrations().Any();

                if (pending)
                {
                    this.Logger.Debug($"No pending migrations found for {typeof(T).Name}.");
                }

                return pending;
            }
            catch (Exception ex)
            {
                this.Logger.Exception(ex);
                return false;
            }
        }
    }
}
