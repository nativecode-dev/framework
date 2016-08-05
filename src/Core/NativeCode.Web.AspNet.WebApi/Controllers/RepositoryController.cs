namespace NativeCode.Web.AspNet.WebApi.Controllers
{
    using NativeCode.Core.Data;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Platform.Logging;

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