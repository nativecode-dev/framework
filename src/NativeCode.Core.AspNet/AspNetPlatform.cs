namespace NativeCode.Core.AspNet
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Dependencies;
    using Microsoft.Extensions.DependencyModel;
    using Platform;

    public class AspNetPlatform : Platform
    {
        public AspNetPlatform(IDependencyContainer container) : base(container)
        {
        }

        protected override IEnumerable<Assembly> GetAssemblies()
        {
            // TODO: Should be optimized to filter first, will fix later.
            return from library in DependencyContext.Default.RuntimeLibraries
                   from assembly in library.Assemblies
                   let ass = Assembly.Load(assembly.Name)
                   select ass;
        }
    }
}