namespace Common.Data
{
    using NativeCode.Core.Providers;
    using NativeCode.Packages.Data;

    public class CoreDataContext : DbDataContext
    {
        public CoreDataContext(IConnectionStringProvider provider) : base(provider)
        {
        }
    }
}