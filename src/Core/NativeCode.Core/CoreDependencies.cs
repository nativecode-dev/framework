namespace NativeCode.Core
{
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Dependencies.Enums;
    using NativeCode.Core.Localization.Translation;
    using NativeCode.Core.Platform.Logging;
    using NativeCode.Core.Platform.Maintenance;
    using NativeCode.Core.Platform.Messaging.Processing;
    using NativeCode.Core.Platform.Security.Authentication;
    using NativeCode.Core.Platform.Security.KeyManagement;
    using NativeCode.Core.Platform.Serialization;
    using NativeCode.Core.Types;
    using NativeCode.Core.Validation;

    public class CoreDependencies : DependencyModule
    {
        public static IDependencyModule Instance => new CoreDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
            RegisterPlatform(registrar);
            RegisterLocalization(registrar);
            RegisterLogging(registrar);
            RegisterSerialization(registrar);
            RegisterValidation(registrar);

            registrar.Register<CancellationTokenManager>();
            registrar.RegisterFactory(resolver => resolver.ResolveAll<IMessageHandler>());
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
            registrar.Register<IKeyManager, KeyManager>();

            registrar.RegisterFactory(resolver => resolver.ResolveAll<IAuthenticationHandler>());
            registrar.RegisterFactory(resolver => resolver.ResolveAll<IMaintenanceProvider>());
            registrar.RegisterFactory(resolver => resolver.ResolveAll<IMessageHandler>());
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