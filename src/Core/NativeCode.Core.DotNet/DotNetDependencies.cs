namespace NativeCode.Core.DotNet
{
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Dependencies.Enums;
    using NativeCode.Core.DotNet.Logging;
    using NativeCode.Core.DotNet.Platform.Connections;
    using NativeCode.Core.DotNet.Platform.Security.Authentication;
    using NativeCode.Core.DotNet.Platform.Security.Authorization;
    using NativeCode.Core.Logging;
    using NativeCode.Core.Platform.Connections;
    using NativeCode.Core.Platform.Security.Authentication;
    using NativeCode.Core.Platform.Security.Authorization;

    public class DotNetDependencies : DependencyModule
    {
        public static IDependencyModule Instance => new DotNetDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
            registrar.Register<IConnectionStringProvider, ConnectionStringProvider>();
            registrar.Register<IHmacSettingsProvider, HmacSettingsProvider>();
            registrar.Register<ILogWriter, TraceLogWriter>(DependencyKey.QualifiedName);
        }

        private static void RegisterAuthentication(IDependencyRegistrar registrar)
        {
            registrar.Register<IAuthenticationProvider, AuthenticationProvider>();
            registrar.Register<IAuthenticationHandler, WindowsAuthenticationHandler>(DependencyKey.QualifiedName);
            registrar.RegisterFactory(resolver => resolver.ResolveAll<IAuthenticationHandler>());
        }
    }
}