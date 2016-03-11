namespace Common.Web
{
    using NativeCode.Core.Dependencies;

    public class CommonWebDependencies : DependencyModule
    {
        public static IDependencyModule Instance => new CommonWebDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
            registrar.RegisterAssembly(this.GetType().Assembly);
        }
    }
}