namespace NativeCode.Core
{
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Localization;
    using NativeCode.Core.Logging;
    using NativeCode.Core.Serialization;

    public class CoreDependencies : DependencyModule
    {
        public static IDependencyModule Instance => new CoreDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
            RegisterLocalization(registrar);
            RegisterLogging(registrar);
            RegisterSerialization(registrar);
        }

        private static void RegisterLocalization(IDependencyRegistrar registrar)
        {
            registrar.Register<ITranslator, Translator>();
            registrar.Register<ITranslationProvider, DefaultTranslationProvider>(lifetime: DependencyLifetime.PerApplication);
        }

        private static void RegisterLogging(IDependencyRegistrar registrar)
        {
            registrar.Register<ILogger, Logger>();
            registrar.Register<ILogWriter, InMemoryLogWriter>();
            registrar.RegisterFactory(x => x.ResolveAll<ILogWriter>());
        }

        private static void RegisterSerialization(IDependencyRegistrar registrar)
        {
            registrar.Register<IStringSerializer, JsonNetStringSerializer>();
        }
    }
}