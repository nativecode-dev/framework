namespace NativeCode.Core.DotNet.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Security.Principal;
    using System.Threading;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Platform;

    public class DotNetPlatform : Platform
    {
        public DotNetPlatform(IDependencyContainer container)
            : base(container)
        {
        }

        public override string ApplicationPath => AppDomain.CurrentDomain.BaseDirectory;

        public override string DataPath => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public override string MachineName => Environment.MachineName;

        public override IEnumerable<Assembly> GetAssemblies(Func<Assembly, bool> filter = null)
        {
            return filter == null ? AppDomain.CurrentDomain.GetAssemblies() : AppDomain.CurrentDomain.GetAssemblies().Where(filter);
        }

        public override IEnumerable<Assembly> GetAssemblies(params string[] prefixes)
        {
            return this.GetAssemblies(x => prefixes.Any(p => x.FullName.Contains(p)));
        }

        public override IPrincipal GetCurrentPrincipal()
        {
            if (string.IsNullOrWhiteSpace(Thread.CurrentPrincipal.Identity.Name))
            {
                return new WindowsPrincipal(WindowsIdentity.GetCurrent());
            }

            return Thread.CurrentPrincipal;
        }
    }
}