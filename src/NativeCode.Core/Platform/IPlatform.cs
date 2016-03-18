namespace NativeCode.Core.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    public interface IPlatform
    {
        [NotNull]
        string ApplicationPath { get; }

        [NotNull]
        string DataPath { get; }

        [NotNull]
        string MachineName { get; }

        Task<IPrincipal> AuthenticateAsync(string login, string password, CancellationToken cancellationToken);

        IEnumerable<Assembly> GetAssemblies(Func<Assembly, bool> filter = null);

        IEnumerable<Assembly> GetAssemblies(params string[] prefixes);

        IPrincipal GetCurrentPrincipal();

        IEnumerable<string> GetCurrentRoles();

        void SetCurrentPrincipal(IPrincipal principal);
    }
}