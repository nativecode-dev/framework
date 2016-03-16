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
    using NativeCode.Core.Platform;

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
                        var application = resolver.Resolve<IApplication>();
                        var token = Guid.NewGuid().ToBase64String();
                        application.Settings.SetValue("Settings.DatabaseVerificationToken", token);

                        return;
                    }
                }

                context.Database.CreateIfNotExists();
                RunMigrations(resolver);
            }
        }

        public static void Upgrade(IDependencyResolver resolver)
        {
            // Ensure that no one else is going to try to upgrade.
            if (Interlocked.CompareExchange(ref upgradable, 0, 1) == 1)
            {
                RunMigrations(resolver);
            }
        }

        private static void RunMigrations(IDependencyResolver resolver)
        {
            using (var context = resolver.Resolve<CoreDataContext>())
            {
                try
                {
                    if (context.Database.Exists())
                    {
                        var configuration = new Configuration();
                        var connection = new DbConnectionInfo(context.Database.Connection.ConnectionString, "System.Data.SqlClient");
                        configuration.TargetDatabase = connection;

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