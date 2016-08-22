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
            registrar.Register<IMessageQueueAdapter, RabbitMessageQueueAdapter>(lifetime: DependencyLifetime.PerApplication);
        }
    }
}
