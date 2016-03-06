namespace Console
{
    using NativeCode.Core.Dependencies;

    public class ConsoleDependencyModule : DependencyModule
    {
        public static IDependencyModule Instance => new ConsoleDependencyModule();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
        }
    }
}