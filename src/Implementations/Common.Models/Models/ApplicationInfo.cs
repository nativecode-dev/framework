namespace Common.Models.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Platform.Connections;

    public class ApplicationInfo
    {
        private static readonly IPlatform Platform = DependencyLocator.Resolver.Resolve<IPlatform>();

        public IEnumerable<AssemblyInfo> Assemblies = Platform.GetAssemblies().Select(x => new AssemblyInfo(x));

        [Display(Description = "Current user roles")]
        public IEnumerable<string> CurrentUserRoles => Platform.GetCurrentRoles();

        [Display(Description = "Current user")]
        public string CurrentUsername => Platform.GetCurrentPrincipal().Identity.Name;

        [Display(Description = "Default connection string")]
        public string DefaultConnectionString => GetDefaultConnectionString();

        [Display(Description = "Version")]
        public string Version => "0.0.0.0";

        private static string GetDefaultConnectionString()
        {
            var provider = DependencyLocator.Resolver.Resolve<IConnectionStringProvider>();
            return provider.GetDefaultConnectionString().ToString();
        }
    }
}