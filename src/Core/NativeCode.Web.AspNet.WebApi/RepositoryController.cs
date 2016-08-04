namespace NativeCode.Web.AspNet.WebApi
{
    using NativeCode.Core.Data;
    using NativeCode.Core.Logging;

    public abstract class RepositoryController<TEntity> : BaseController
        where TEntity : Entity
    {
        protected RepositoryController(IRepository<TEntity> repository, ILogger logger)
            : base(logger)
        {
            this.Repository = repository;
            this.Disposable(repository);
        }

        protected IRepository<TEntity> Repository { get; }
    }
}