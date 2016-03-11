namespace NativeCode.Core.DotNet
{
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Dependencies.Enums;
    using NativeCode.Core.DotNet.Logging;
    using NativeCode.Core.DotNet.Platform;
    using NativeCode.Core.DotNet.Providers;
    using NativeCode.Core.Logging;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Providers;

    public class DotNetDependencies : DependencyModule
    {
        public static IDependencyModule Instance => new DotNetDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
            registrar.Register<IConnectionStringProvider, ConnectionStringProvider>();
            registrar.Register<ILogWriter, TraceLogWriter>(DependencyKey.QualifiedName);
            registrar.Register<IPlatform, DotNetPlatform>();
            registrar.Register<IPrincipalInflater, WindowsPrincipalInflater>(PrincipalSource.Windows.ToString());
        }
    }
}