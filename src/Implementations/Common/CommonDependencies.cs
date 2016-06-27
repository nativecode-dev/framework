namespace Common
{
    using Common.Data;
    using Common.Data.Entities;
    using Common.Data.Entities.Security;
    using Common.Data.Entities.Storage;
    using Common.DataServices;
    using Common.Workers;

    using NativeCode.Core.Data;
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Dependencies.Enums;

    public class CommonDependencies : DependencyModule
    {
        public static IDependencyModule Instance => new CommonDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
            registrar.Register<CoreDataContext>(lifetime: DependencyLifetime.PerResolve);

            registrar.Register<IAccountService, AccountService>(lifetime: DependencyLifetime.PerResolve);
            registrar.Register<IDownloadService, DownloadService>(lifetime: DependencyLifetime.PerResolve);
            registrar.Register<IStorageService, StorageService>(lifetime: DependencyLifetime.PerResolve);
            registrar.Register<ITokenService, TokenService>(lifetime: DependencyLifetime.PerResolve);

            registrar.Register<IRepository<Account, CoreDataContext>, Repository<Account, CoreDataContext>>(lifetime: DependencyLifetime.PerResolve);
            registrar.Register<IRepository<Download, CoreDataContext>, Repository<Download, CoreDataContext>>(lifetime: DependencyLifetime.PerResolve);
            registrar.Register<IRepository<Storage, CoreDataContext>, Repository<Storage, CoreDataContext>>(lifetime: DependencyLifetime.PerResolve);
            registrar.Register<IRepository<Token, CoreDataContext>, Repository<Token, CoreDataContext>>(lifetime: DependencyLifetime.PerResolve);

            registrar.Register<IWorkManager<Download>, DownloadWorkManager>(lifetime: DependencyLifetime.PerApplication);
            registrar.Register<IWorkProvider<Download>, DownloadWorkProvider>();
        }
    }
}