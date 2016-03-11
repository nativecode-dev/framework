namespace Common.Data
{
    using System.Data.Entity;

    using Common.Data.Entities;

    using NativeCode.Core.DotNet.Platform;
    using NativeCode.Core.DotNet.Providers;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Providers;
    using NativeCode.Packages.Data;

    public class CoreDataContext : DbDataContext
    {
        public CoreDataContext()
            : base(new ConnectionStringProvider(), new DotNetPlatform())
        {
        }

        public CoreDataContext(IConnectionStringProvider provider, IPlatform platform)
            : base(provider, platform)
        {
        }

        public IDbSet<Account> Accounts { get; set; }

        public IDbSet<Download> Downloads { get; set; }

        public IDbSet<Token> Tokens { get; set; }
    }
}