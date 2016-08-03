﻿namespace NativeCode.Core.Packages.EntityFramework
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using NativeCode.Core.Logging;
    using NativeCode.Core.Platform.Connections;
    using NativeCode.Core.Platform.Maintenance;

    public class DbMaintainUpgradeState<T> : IMaintainUpgradeState
        where T : DbDataContext
    {
        public DbMaintainUpgradeState(IConnectionStringProvider connections, ILogger logger)
        {
            this.Logger = logger;

            if (this.HasPendingMigrations(connections.GetDefaultConnectionString()))
            {
                this.EnterMaintenance();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IMaintainUpgradeState" /> is maintenance.
        /// </summary>
        public bool Active { get; private set; }

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
            this.Logger.Informational($"Checking for pending migrations using ({connectionString}) with [{providerName}].");

            try
            {
                var configuration = new DbMigrationsConfiguration<T> { TargetDatabase = new DbConnectionInfo(connectionString, providerName) };
                var migration = new DbMigrator(configuration);

                return migration.GetPendingMigrations().Any();
            }
            catch (Exception ex)
            {
                this.Logger.Exception(ex);
                return false;
            }
        }
    }
}
