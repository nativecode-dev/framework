namespace NativeCode.Core.Packages.EntityFramework
{
    using System.Data.Entity.Infrastructure;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.DotNet.Platform;
    using NativeCode.Core.DotNet.Platform.Connections;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Platform.Connections;

    public abstract class DbContextFactory<TDataContext> : IDbContextFactory<TDataContext>
        where TDataContext : DbDataContext
    {
        public TDataContext Create()
        {
            return this.CreateDataContext(new ShimConnectionStringProvider(), new ShimPlatform(this.CreateDependencyContainer()));
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
