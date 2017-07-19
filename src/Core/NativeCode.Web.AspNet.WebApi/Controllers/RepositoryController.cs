namespace NativeCode.Web.AspNet.WebApi.Controllers
{
    using Core.Data;
    using Core.Platform;
    using Core.Platform.Logging;

    public abstract class RepositoryController<TEntity> : BaseController
        where TEntity : Entity
    {
        protected RepositoryController(IApplication application, ILogger logger, IRepository<TEntity> repository)
            : base(application, logger)
        {
            this.Repository = repository;
            this.EnsureDisposed(repository);
        }

        protected IRepository<TEntity> Repository { get; }
    }
}