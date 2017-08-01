namespace NativeCode.Core.DotNet.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Security.Principal;
    using System.Threading;
    using Core.Platform;
    using Dependencies;

    public class DotNetPlatform : Platform
    {
        public DotNetPlatform(IDependencyContainer container)
            : base(container)
        {
        }

        public override string BinariesPath => AppDomain.CurrentDomain.BaseDirectory;

        public override string DataPath => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public override string MachineName => Environment.MachineName;

        public override IEnumerable<Assembly> GetAssemblies(Func<Assembly, bool> filter = null)
        {
            return filter == null
                ? AppDomain.CurrentDomain.GetAssemblies()
                : AppDomain.CurrentDomain.GetAssemblies().Where(filter);
        }

        public override IEnumerable<Assembly> GetAssemblies(params string[] prefixes)
        {
            return this.GetAssemblies(x => prefixes.Any(p => x.FullName.Contains(p)));
        }

        public override IPrincipal GetCurrentPrincipal()
        {
            if (string.IsNullOrWhiteSpace(Thread.CurrentPrincipal?.Identity?.Name))
                try
                {
                    return new WindowsPrincipal(WindowsIdentity.GetCurrent());
                }
                catch
                {
                    // Whelp, let's give up because GetCurrent failed the call.
                    // We don't need no stinkin' exceptions!
                }

            return Thread.CurrentPrincipal;
        }
    }
}