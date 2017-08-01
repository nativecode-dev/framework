namespace Cavern.Data
{
    using NativeCode.Core.Data;
    using NativeCode.Core.Packages.EntityFramework;

    public abstract class ScraperDataService<T> : DbRepository<T, ScraperContext> where T : Entity
    {
        protected ScraperDataService(ScraperContext context) : base(context)
        {
        }
    }
}