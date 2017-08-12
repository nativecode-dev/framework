namespace NativeCode.Core.Platform
{
    using System;
    using System.Security.Principal;

    public interface IUserScope : IDisposable
    {
        IPrincipal Principal { get; }
    }
}