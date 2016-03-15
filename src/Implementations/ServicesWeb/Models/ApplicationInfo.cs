namespace ServicesWeb.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Web;
    using System.Web.Compilation;
    using System.Web.Security;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Platform.Connections;

    public class ApplicationInfo
    {
        public IEnumerable<AssemblyInfo> Assemblies = GetAssemblyNames();

        [DisplayName("Current user roles")]
        public IEnumerable<string> CurrentUserRoles => GetCurrentUserRoles();

        [DisplayName("Current user")]
        public string CurrentUsername => GetCurrentUsername();

        [DisplayName("Default connection string")]
        public string DefaultConnectionString => GetDefaultConnectionString();

        [DisplayName("Version")]
        public string Version => GetAssemblyVersion();

        private static IEnumerable<AssemblyInfo> GetAssemblyNames()
        {
            return from Assembly assembly in BuildManager.GetReferencedAssemblies()
                   let name = assembly.GetName().Name
                   let attributes = assembly.GetCustomAttributes().Select(x => x.GetType()).OrderBy(x => x.Name).Select(x => x.Name)
                   orderby name
                   select new AssemblyInfo { Attributes = attributes, FullName = assembly.FullName, Name = name, Path = assembly.Location };
        }

        private static string GetAssemblyVersion()
        {
            var attribute = BuildManager.GetGlobalAsaxType().Assembly.GetCustomAttribute<AssemblyVersionAttribute>();

            if (attribute != null)
            {
                return attribute.Version;
            }

            return "0.0.0.0";
        }

        private static IEnumerable<string> GetCurrentUserRoles()
        {
            return Roles.GetRolesForUser(GetCurrentUsername());
        }

        private static string GetCurrentUsername()
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.User.Identity.Name;
            }

            return Thread.CurrentPrincipal.Identity.Name;
        }

        private static string GetDefaultConnectionString()
        {
            var provider = DependencyLocator.Resolver.Resolve<IConnectionStringProvider>();
            return provider.GetDefaultConnectionString().ToString();
        }
    }
}