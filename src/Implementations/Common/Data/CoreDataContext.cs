namespace Common.Data
{
    using System.Data.Entity;

    using Common.Data.Entities.Navigation;
    using Common.Data.Entities.Security;
    using Common.Data.Entities.Storage;

    using NativeCode.Core.DotNet.Platform;
    using NativeCode.Core.DotNet.Providers;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Platform.Connections;
    using NativeCode.Packages.Data;
    using NativeCode.Packages.Dependencies;

    public class CoreDataContext : DbDataContext
    {
        public CoreDataContext()
            : base(new ConnectionStringProvider(), new DotNetPlatform(new UnityDependencyContainer()))
        {
        }

        public CoreDataContext(IConnectionStringProvider provider, IPlatform platform)
            : base(provider, platform)
        {
        }

        public virtual IDbSet<Account> Accounts { get; set; }

        public virtual IDbSet<Download> Downloads { get; set; }

        public virtual IDbSet<NavigationItem> NavigationItems { get; set; }

        public virtual IDbSet<Storage> Storage { get; set; }

        public virtual IDbSet<Token> Tokens { get; set; }
    }
}