namespace NativeCode.Core.Platform
{
    using System;
    using System.Security.Principal;
    using Dependencies;

    public interface IApplicationScope : IDisposable
    {
        IApplicationScope CreateChildScope();

        IDependencyResolver CreateResolver();

        IUserScope CreateUserScope(IPrincipal principal);
    }
}