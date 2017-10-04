namespace NativeCode.Core.Platform
{
    using System;
    using System.Security.Principal;
    using JetBrains.Annotations;

    public interface IUserScope : IDisposable
    {
        [NotNull]
        IPrincipal Principal { get; }
    }
}