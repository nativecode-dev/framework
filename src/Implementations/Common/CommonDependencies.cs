namespace Common
{
    using Common.Data;
    using Common.Data.Entities;
    using Common.DataServices;
    using Common.Workers;

    using NativeCode.Core.Data;
    using NativeCode.Core.Dependencies;

    public class CommonDependencies : DependencyModule
    {
        public static IDependencyModule Instance => new CommonDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
            registrar.Register<CoreDataContext>(lifetime: this.PerContainer);

            registrar.Register<IAccountService, AccountService>();
            registrar.Register<IDownloadService, DownloadService>();
            registrar.Register<ITokenService, TokenService>();

            registrar.Register<IRepository<Account, CoreDataContext>, Repository<Account, CoreDataContext>>(lifetime: this.PerContainer);
            registrar.Register<IRepository<Download, CoreDataContext>, Repository<Download, CoreDataContext>>(lifetime: this.PerContainer);
            registrar.Register<IRepository<Token, CoreDataContext>, Repository<Token, CoreDataContext>>(lifetime: this.PerContainer);

            registrar.Register<IWorkManager<Download>, DownloadWorkManager>(lifetime: this.PerApplication);
            registrar.Register<IWorkProvider<Download>, DownloadWorkProvider>();
        }
    }
}