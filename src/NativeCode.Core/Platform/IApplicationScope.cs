namespace NativeCode.Core.Platform
{
    using System;
    using System.Security.Principal;

    public interface IApplicationScope : IDisposable
    {
        IApplicationScope CreateChildScope();

        IUserScope CreateUserScope(IPrincipal principal);
    }
}