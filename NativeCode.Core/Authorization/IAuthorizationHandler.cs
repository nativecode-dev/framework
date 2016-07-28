namespace NativeCode.Core.Authorization
{
    public interface IAuthorizationHandler
    {
        void AssertDeny(string requirements, object[] parameters);
    }
}