namespace NativeCode.Core
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Dependencies;

    public class CorePlatform<TSettings> : Platform.Platform where TSettings : Settings.Settings
    {
        public CorePlatform(IDependencyContainer container) : base(container)
        {
        }

        protected override IEnumerable<Assembly> GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }
    }
}