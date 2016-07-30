namespace NativeCode.Core
{
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Dependencies.Enums;
    using NativeCode.Core.Localization.Translation;
    using NativeCode.Core.Logging;
    using NativeCode.Core.Platform.Security;
    using NativeCode.Core.Platform.Security.Authentication;
    using NativeCode.Core.Serialization;
    using NativeCode.Core.Validation;

    public class CoreDependencies : DependencyModule
    {
        public static IDependencyModule Instance => new CoreDependencies();

        public override void RegisterDependencies(IDependencyRegistrar registrar)
        {
            this.RegisterPlatform(registrar);
            this.RegisterLocalization(registrar);
            this.RegisterLogging(registrar);
            this.RegisterSerialization(registrar);
            this.RegisterValidation(registrar);
        }

        protected virtual void RegisterLocalization(IDependencyRegistrar registrar)
        {
            registrar.Register<ITranslator, Translator>();
            registrar.Register<ITranslationProvider, TranslationProvider>(lifetime: DependencyLifetime.PerApplication);
        }

        protected virtual void RegisterLogging(IDependencyRegistrar registrar)
        {
            registrar.Register<ILogger, Logger>();
            registrar.Register<ILogWriter, InMemoryLogWriter>(DependencyKey.QualifiedName);
            registrar.RegisterFactory(x => x.ResolveAll<ILogWriter>(), lifetime: DependencyLifetime.PerResolve);
        }

        protected virtual void RegisterPlatform(IDependencyRegistrar registrar)
        {
            registrar.RegisterFactory(x => x.ResolveAll<IAuthenticationProvider>(), lifetime: DependencyLifetime.PerResolve);
        }

        protected virtual void RegisterSerialization(IDependencyRegistrar registrar)
        {
            registrar.Register<IStringSerializer, JsonNetStringSerializer>();
        }

        protected virtual void RegisterValidation(IDependencyRegistrar registrar)
        {
            registrar.Register<IObjectValidator, StringComplexityValidator>();
        }
    }
}