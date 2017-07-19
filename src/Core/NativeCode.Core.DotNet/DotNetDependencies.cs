namespace NativeCode.Core.DotNet
{
    using Core.Platform.Connections;
    using Core.Platform.FileSystem;
    using Core.Platform.Logging;
    using Core.Platform.Security.Authentication;
    using Core.Platform.Security.Authorization;
    using Dependencies;
    using Dependencies.Enums;
    using Logging;
    using Platform.Connections;
    using Platform.FileSystem;
    using Platform.Security.Authentication;
    using Platform.Security.Authorization;

    public class DotNetDependencies : DependencyModule
    {
        public static IDependencyModule Instance => new DotNetDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
            registrar.Register<IConnectionStringProvider, ConnectionStringProvider>();
            registrar.Register<IFileSystem, DotNetFileSystem>();
            registrar.Register<IHmacSettingsProvider, HmacSettingsProvider>();
            registrar.Register<ILogWriter, TraceLogWriter>(DependencyKey.QualifiedName);

            RegisterAuthentication(registrar);
        }

        private static void RegisterAuthentication(IDependencyRegistrar registrar)
        {
            registrar.Register<IAuthenticationProvider, AuthenticationProvider>();
            registrar.Register<IAuthenticationHandler, WindowsAuthenticationHandler>(DependencyKey.QualifiedName);
            registrar.RegisterFactory(resolver => resolver.ResolveAll<IAuthenticationHandler>());
        }
    }
}