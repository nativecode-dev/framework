namespace NativeCode.Core
{
    using Dependencies;
    using Dependencies.Enums;
    using Localization.Translation;
    using Platform.Logging;
    using Platform.Maintenance;
    using Platform.Messaging.Queuing;
    using Platform.Security.Authentication;
    using Platform.Security.KeyManagement;
    using Platform.Serialization;
    using Types;
    using Validation;

    public class CoreDependencies : DependencyModule
    {
        public static IDependencyModule Instance => new CoreDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
            RegisterLocalization(registrar);
            RegisterLogging(registrar);
            RegisterPlatform(registrar);
            RegisterQueueing(registrar);
            RegisterSerialization(registrar);
            RegisterValidation(registrar);
        }

        private static void RegisterLocalization(IDependencyRegistrar registrar)
        {
            registrar.Register<ITranslator, Translator>();
            registrar.Register<ITranslationProvider, TranslationProvider>();
        }

        private static void RegisterLogging(IDependencyRegistrar registrar)
        {
            registrar.Register<ILogger, Logger>();
            registrar.Register<ILogWriter, NullLogWriter>(DependencyKey.QualifiedName);

            registrar.RegisterFactory(resolver => resolver.ResolveAll<ILogWriter>());
        }

        private static void RegisterPlatform(IDependencyRegistrar registrar)
        {
            registrar.Register<ICancellationTokenManager, CancellationTokenManager>();
            registrar.Register<IKeyManager, KeyManager>();

            registrar.RegisterFactory(resolver => resolver.ResolveAll<IAuthenticationHandler>());
            registrar.RegisterFactory(resolver => resolver.ResolveAll<IMaintenanceProvider>());
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