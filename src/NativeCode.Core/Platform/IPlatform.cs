namespace NativeCode.Core.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using NativeCode.Core.Dependencies;

    public interface IPlatform : IDisposable
    {
        [NotNull]
        string ApplicationPath { get; }

        [NotNull]
        string DataPath { get; }

        [NotNull]
        string MachineName { get; }

        IDependencyRegistrar Registrar { get; }

        IDependencyResolver Resolver { get; }

        Task<IPrincipal> AuthenticateAsync(string login, string password, CancellationToken cancellationToken);

        IDependencyContainer CreateDependencyScope(IDependencyContainer parent = default(IDependencyContainer));

        IEnumerable<Assembly> GetAssemblies(Func<Assembly, bool> filter = null);

        IEnumerable<Assembly> GetAssemblies(params string[] prefixes);

        IPrincipal GetCurrentPrincipal();

        IEnumerable<string> GetCurrentRoles();

        void SetCurrentPrincipal(IPrincipal principal);
    }
}