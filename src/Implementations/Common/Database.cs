namespace Common
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Threading;

    using Common.Data;
    using Common.Migrations;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Extensions;

    public static class Database
    {
        private static int upgradable;

        public static bool UpgradeRequired => upgradable != 0;

        public static void Configure(IDependencyResolver resolver)
        {
            using (var context = resolver.Resolve<CoreDataContext>())
            {
                if (context.Database.Exists() && context.Database.CompatibleWithModel(true).Not())
                {
                    if (Interlocked.CompareExchange(ref upgradable, 1, 0) == 0)
                    {
                        return;
                    }
                }

                context.Database.CreateIfNotExists();
            }
        }

        public static void Upgrade(IDependencyResolver resolver)
        {
            // Ensure that no one else is going to try to upgrade.
            if (Interlocked.CompareExchange(ref upgradable, 0, 1) == 1)
            {
                using (var context = resolver.Resolve<CoreDataContext>())
                {
                    try
                    {
                        if (context.Database.Exists())
                        {
                            var configuration = new Configuration();
                            configuration.TargetDatabase = new DbConnectionInfo(context.Database.Connection.ConnectionString, "System.Data.SqlClient");

                            var migrator = new DbMigrator(configuration);
                            migrator.Update();
                        }
                    }
                    catch (Exception ex)
                    {
                        Interlocked.CompareExchange(ref upgradable, 1, 0);
                        Trace.WriteLine(ex);
                    }
                }
            }
        }
    }
}