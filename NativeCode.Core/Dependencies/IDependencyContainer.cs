namespace NativeCode.Core.Dependencies
{
    using System;

    public interface IDependencyContainer : IDisposable
    {
        IDependencyRegistrar Registrar { get; }

        IDependencyResolver Resolver { get; }

        IDependencyContainer CreateChildContainer();
    }
}