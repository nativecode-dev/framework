namespace NativeCode.Core.Packages.Rabbit
{
    using Dependencies;
    using Dependencies.Enums;
    using Platform.Messaging.Queuing;

    public class RabbitDependencies : DependencyModule
    {
        public static readonly IDependencyModule Instance = new RabbitDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
            registrar.Register<IMessageQueueAdapter, RabbitMessageQueueAdapter>(lifetime: DependencyLifetime
                .PerApplication);
        }
    }
}