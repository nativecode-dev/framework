namespace NativeCode.Core.Web
{
    using Dependencies;

    public class CoreWebDependencies : DependencyModule
    {
        public static IDependencyModule Instance => new CoreWebDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
        }
    }
}