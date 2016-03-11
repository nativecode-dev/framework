namespace NativeCode.Core.Platform
{
    using System.Security.Principal;

    using JetBrains.Annotations;

    public interface IPlatform
    {
        [NotNull]
        string ApplicationPath { get; }

        [NotNull]
        string DataPath { get; }

        [NotNull]
        string MachineName { get; }

        IPrincipal GetCurrentPrincipal();

        void SetCurrentPrincipal(IPrincipal principal);
    }
}