namespace NativeCode.Core.Web
{
    using NativeCode.Core.Dependencies;

    public class WebDependencies : DependencyModule
    {
        public static IDependencyModule Instance => new WebDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
        }
    }
}