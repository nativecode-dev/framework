namespace NativeCode.Web
{
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Platform;
    using NativeCode.Web.Platform;

    public class WebDependencies : DependencyModule
    {
        public static IDependencyModule Instance => new WebDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
            registrar.Register<IPlatform, WebPlatform>();
        }
    }
}