namespace NativeCode.Core
{
    using Dependencies;
    using Localization.Translation;
    using Platform.Connections;
    using Platform.Messaging.Queuing;
    using Platform.Security.KeyManagement;
    using Platform.Serialization;
    using Reliability;
    using Validation;

    public class CoreDependencies : DependencyModule
    {
        public static IDependencyModule Instance => new CoreDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
            CoreDependencies.RegisterLocalization(registrar);
            CoreDependencies.RegisterPlatform(registrar);
            CoreDependencies.RegisterQueueing(registrar);
            CoreDependencies.RegisterSerialization(registrar);
            CoreDependencies.RegisterValidation(registrar);
        }

        private static void RegisterLocalization(IDependencyRegistrar registrar)
        {
            registrar.Register<ITranslator, Translator>();
            registrar.Register<ITranslationProvider, TranslationProvider>();
        }

        private static void RegisterPlatform(IDependencyRegistrar registrar)
        {
            registrar.Register<ICancellationTokenManager, CancellationTokenManager>();
            registrar.Register<IConnectionStringProvider, SettingsConnectionStringProvider>();
            registrar.Register<IKeyManager, KeyManager>();

            registrar.Register<SettingsConnectionStringProvider>();
        }

        private static void RegisterQueueing(IDependencyRegistrar registrar)
        {
            registrar.Register<IMessageQueueAdapter, MessageQueueAdapter>();
        }

        private static void RegisterSerialization(IDependencyRegistrar registrar)
        {
            registrar.Register<IStringSerializer, JsonStringSerializer>();
        }

        private static void RegisterValidation(IDependencyRegistrar registrar)
        {
            registrar.Register<IObjectValidator, StringComplexityValidator>();
        }
    }
}