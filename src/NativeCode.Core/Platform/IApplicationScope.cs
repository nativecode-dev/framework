namespace NativeCode.Core.Platform
{
    using System;
    using System.Security.Principal;
    using Dependencies;
    using JetBrains.Annotations;

    public interface IApplicationScope : IDisposable
    {
        [NotNull]
        IApplicationScope CreateChildScope();

        [NotNull]
        IDependencyResolver CreateResolver();

        [NotNull]
        IUserScope CreateUserScope([NotNull] IPrincipal principal);
    }
}