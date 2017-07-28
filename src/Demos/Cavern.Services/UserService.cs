namespace Cavern.Services
{
    using Data;
    using Data.Security;
    using NativeCode.Core.Dependencies.Attributes;

    [Dependency(typeof(IUserService))]
    public class UserService : ScraperContextService<User>, IUserService
    {
        public UserService(ScraperContext context) : base(context)
        {
        }
    }

    public interface IUserService : IService
    {
    }
}
