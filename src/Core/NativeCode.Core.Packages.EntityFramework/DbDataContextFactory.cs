namespace NativeCode.Core.Packages.EntityFramework
{
    using System;
    using System.Data.Entity.Infrastructure;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.DotNet.Platform;
    using NativeCode.Core.DotNet.Platform.Connections;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Platform.Connections;

    public abstract class DbContextFactory<TDataContext> : IDbContextFactory<TDataContext>
        where TDataContext : DbDataContext
    {
        public TDataContext Create()
        {
            IDependencyContainer container = null;
            IPlatform platform = null;

            try
            {
                var provider = new ShimConnectionStringProvider();
                container = this.CreateDependencyContainer();
                platform = new ShimPlatform(container);

                return this.CreateDataContext(provider, platform);
            }
            catch (Exception ex)
            {
                container?.Dispose();
                platform?.Dispose();
                throw;
            }
        }

        protected abstract TDataContext CreateDataContext(IConnectionStringProvider connectionStringProvider, IPlatform platform);

        protected abstract IDependencyContainer CreateDependencyContainer();

        private class ShimPlatform : DotNetPlatform
        {
            public ShimPlatform(IDependencyContainer container)
                : base(container)
            {
            }
        }

        private class ShimConnectionStringProvider : ConnectionStringProvider
        {
            public override ConnectionString GetDefaultConnectionString()
            {
                return new SqlServerConnectionString(this.GetConnectionString<TDataContext>());
            }
        }
    }
}
