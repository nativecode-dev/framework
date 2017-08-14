namespace NativeCode.Core.AspNet
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Dependencies;
    using Platform;

    public class AspNetPlatform : Platform
    {
        public AspNetPlatform(IDependencyContainer container) : base(container)
        {
        }

        protected override IEnumerable<Assembly> GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }
    }
}