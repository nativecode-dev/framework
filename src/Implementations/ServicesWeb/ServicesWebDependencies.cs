namespace ServicesWeb
{
    using NativeCode.Core.Dependencies;

    public class ServicesWebDependencies : DependencyModule
    {
        public static IDependencyModule Instance = new ServicesWebDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
        }
    }
}