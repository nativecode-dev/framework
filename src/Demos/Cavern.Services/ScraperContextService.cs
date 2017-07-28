namespace Cavern.Services
{
    using Data;
    using NativeCode.Core.Data;
    using NativeCode.Core.Packages.EntityFramework;

    public abstract class ScraperContextService<T> : DbRepository<T, ScraperContext> where T : Entity
    {
        protected ScraperContextService(ScraperContext context) : base(context)
        {
        }
    }
}