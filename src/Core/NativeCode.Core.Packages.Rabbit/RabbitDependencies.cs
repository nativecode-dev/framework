namespace NativeCode.Core.Packages.Rabbit
{
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Dependencies.Enums;
    using NativeCode.Core.Platform.Messaging.Queuing;

    public class RabbitDependencies : DependencyModule
    {
        public static readonly IDependencyModule Instance = new RabbitDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
            registrar.Register<RabbitMessageQueueFactory>(lifetime: DependencyLifetime.PerApplication);
            registrar.RegisterFactory<IMessageQueueFactory>(resolver => resolver.Resolve<RabbitMessageQueueFactory>());
            registrar.RegisterFactory<IMessageQueueFactory<RabbitMessageQueueOptions>>(resolver => resolver.Resolve<RabbitMessageQueueFactory>());
        }
    }
}
