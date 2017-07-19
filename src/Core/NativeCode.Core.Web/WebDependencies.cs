namespace NativeCode.Core.Web
{
    using Core.Platform.FileSystem;
    using Dependencies;
    using Platform.FileSystem;

    public class WebDependencies : DependencyModule
    {
        public static IDependencyModule Instance => new WebDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
            registrar.Register<IFileInspector, WebFileInspector>();
        }
    }
}