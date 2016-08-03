namespace NativeCode.Core.Extensions
{
    using System.Linq;
    using System.Reflection;

    public static class AssemblyExtensions
    {
        public static string GetAssemblyFileVersion(this Assembly assembly)
        {
            var attribute = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();

            if (attribute != null)
            {
                return attribute.Version;
            }

            return string.Empty;
        }

        public static string GetAssemblyInformationalVersion(this Assembly assembly)
        {
            var attribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();

            if (attribute != null)
            {
                return attribute.InformationalVersion;
            }

            return string.Empty;
        }

        public static string GetAssemblyVersion(this Assembly assembly)
        {
            var attribute = assembly.GetCustomAttribute<AssemblyVersionAttribute>();

            if (attribute != null)
            {
                return attribute.Version;
            }

            return string.Empty;
        }

        public static string GetVersion(this Assembly assembly)
        {
            var versions = new[] { assembly.GetAssemblyFileVersion(), assembly.GetAssemblyInformationalVersion(), assembly.GetAssemblyVersion() };

            return versions.FirstOrDefault(version => string.IsNullOrWhiteSpace(version) == false);
        }
    }
}
