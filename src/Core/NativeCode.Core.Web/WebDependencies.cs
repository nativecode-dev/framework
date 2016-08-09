namespace NativeCode.Core.Web
{
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Platform.FileSystem;
    using NativeCode.Core.Web.Platform.FileSystem;

    public class WebDependencies : DependencyModule
    {
        public static IDependencyModule Instance => new WebDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
            registrar.Register<IFileInspector, WebFileInspector>();
        }
    }
}