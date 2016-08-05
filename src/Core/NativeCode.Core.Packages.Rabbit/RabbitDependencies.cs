namespace NativeCode.Core.Packages.Rabbit
{
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Messaging.Queuing;

    public class RabbitDependencies : DependencyModule
    {
        public static readonly IDependencyModule Instance = new RabbitDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
            registrar.Register<IMessageQueueFactory, RabbitMessageQueueFactory>();
        }
    }
}
