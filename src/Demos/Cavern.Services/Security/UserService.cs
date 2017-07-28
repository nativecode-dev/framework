namespace Cavern.Services.Security
{
    using Data;
    using Data.Security;
    using NativeCode.Core.Dependencies.Attributes;

    [Dependency(typeof(IUserService))]
    public class UserService : ScraperDataService<User>, IUserService
    {
        public UserService(ScraperContext context) : base(context)
        {
        }
    }
}
