﻿namespace NativeCode.Core.DotNet
{
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Dependencies.Enums;
    using NativeCode.Core.DotNet.Logging;
    using NativeCode.Core.DotNet.Platform;
    using NativeCode.Core.DotNet.Platform.Security;
    using NativeCode.Core.DotNet.Providers;
    using NativeCode.Core.Logging;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Platform.Connections;
    using NativeCode.Core.Platform.Security;

    public class DotNetDependencies : DependencyModule
    {
        public static IDependencyModule Instance => new DotNetDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
            registrar.Register<IAuthenticationProvider, WindowsAuthenticationProvider>(DependencyKey.QualifiedName);
            registrar.Register<IConnectionStringProvider, ConnectionStringProvider>();
            registrar.Register<IHmacSettingsProvider, HmacSettingsProvider>();
            registrar.Register<ILogWriter, TraceLogWriter>(DependencyKey.QualifiedName);
            registrar.Register<IPlatform, DotNetPlatform>(lifetime: DependencyLifetime.PerApplication);
        }
    }
}