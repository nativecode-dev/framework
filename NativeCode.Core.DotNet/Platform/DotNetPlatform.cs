namespace NativeCode.Core.DotNet.Platform
{
    using System;
    using System.Collections.Generic;
    using System.DirectoryServices.AccountManagement;
    using System.Linq;
    using System.Reflection;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Platform.Security;

    using Principal = NativeCode.Core.Platform.Principal;

    public class DotNetPlatform : Platform
    {
        public DotNetPlatform(IDependencyContainer container)
            : base(container)
        {
        }

        public override string ApplicationPath => AppDomain.CurrentDomain.BaseDirectory;

        public override string DataPath => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public override string MachineName => Environment.MachineName;

        public static IEnumerable<string> GetActiveDirectoryGroups([NotNull] IPrincipal principal)
        {
            var result = new List<string>();

            if (principal.IsAuthenticated(true))
            {
                UserLoginName userLoginName;

                if (UserLoginName.TryParse(principal.Identity.Name, out userLoginName))
                {
                    var domain = userLoginName.Domain;

                    if (string.IsNullOrWhiteSpace(domain))
                    {
                        var application = DependencyLocator.Resolver.Resolve<IApplication>();
                        domain = application.Settings.GetValue("Global.DefaultDomain", ".");
                    }

                    using (var context = new PrincipalContext(ContextType.Domain, domain))
                    {
                        using (var user = UserPrincipal.FindByIdentity(context, userLoginName.Login))
                        {
                            if (user == null)
                            {
                                return result;
                            }

                            using (var groups = user.GetAuthorizationGroups())
                            {
                                result.AddRange(groups.OfType<GroupPrincipal>().Select(item => item.Name));
                            }
                        }
                    }
                }
            }

            return result;
        }

        public override IEnumerable<Assembly> GetAssemblies(Func<Assembly, bool> filter = null)
        {
            return filter == null ? AppDomain.CurrentDomain.GetAssemblies() : AppDomain.CurrentDomain.GetAssemblies().Where(filter);
        }

        public override IEnumerable<Assembly> GetAssemblies(params string[] prefixes)
        {
            return this.GetAssemblies(x => prefixes.Any(p => x.FullName.Contains(p)));
        }

        public override async Task<IPrincipal> AuthenticateAsync(string login, string password, CancellationToken cancellationToken)
        {
            // TODO: Service location, kinda ugly...can we do better?
            var authenticators = this.Resolver.ResolveAll<IAuthenticationProvider>();

            foreach (var authenticator in authenticators)
            {
                if (!authenticator.CanHandle(login))
                {
                    continue;
                }

                var response = await authenticator.AuthenticateAsync(login, password, cancellationToken).ConfigureAwait(false);

                if (response.Result == AuthenticationResultType.Authenticated)
                {
                    return response.Principal;
                }
            }

            return null;
        }

        public override IPrincipal GetCurrentPrincipal()
        {
            if (string.IsNullOrWhiteSpace(Thread.CurrentPrincipal.Identity.Name))
            {
                var identity = WindowsIdentity.GetCurrent();

                if (identity != null)
                {
                    return new WindowsPrincipal(identity);
                }

                return Principal.Anonymous;
            }

            return Thread.CurrentPrincipal;
        }

        public override IEnumerable<string> GetCurrentRoles()
        {
            var principal = this.GetCurrentPrincipal();

            if (principal != null)
            {
                return GetActiveDirectoryGroups(principal);
            }

            return Enumerable.Empty<string>();
        }

        public override void SetCurrentPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
        }
    }
}