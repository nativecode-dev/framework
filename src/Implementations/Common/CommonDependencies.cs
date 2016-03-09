namespace Common
{
    using Common.Data;

    using NativeCode.Core.Dependencies;

    public class CommonDependencies : DependencyModule
    {
        public static IDependencyModule Instance => new CommonDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
            registrar.Register<CoreDataContext>(lifetime: DependencyLifetime.PerContainer);
        }
    }
}