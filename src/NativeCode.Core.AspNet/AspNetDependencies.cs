namespace NativeCode.Core.AspNet
{
    using Dependencies;

    public class AspNetDependencies : DependencyModule
    {
        public static IDependencyModule Instance => new AspNetDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
        }
    }
}