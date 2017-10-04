namespace NativeCode.Core.Extensions
{
    using System.Linq;
    using System.Reflection;

    public static class AssemblyExtensions
    {
        public static string GetAssemblyFileVersion(this Assembly assembly)
        {
            var attribute = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();

            return attribute != null ? attribute.Version : string.Empty;
        }

        public static string GetAssemblyInformationalVersion(this Assembly assembly)
        {
            var attribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();

            return attribute != null ? attribute.InformationalVersion : string.Empty;
        }

        public static string GetAssemblyVersion(this Assembly assembly)
        {
            var attribute = assembly.GetCustomAttribute<AssemblyVersionAttribute>();

            return attribute != null ? attribute.Version : string.Empty;
        }

        public static string GetVersion(this Assembly assembly)
        {
            var versions = new[]
            {
                assembly.GetAssemblyFileVersion(),
                assembly.GetAssemblyInformationalVersion(),
                assembly.GetAssemblyVersion()
            };

            return versions.FirstOrDefault(version => string.IsNullOrWhiteSpace(version) == false);
        }
    }
}