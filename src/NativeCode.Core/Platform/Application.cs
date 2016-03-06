namespace NativeCode.Core.Platform
{
    using NativeCode.Core.Dependencies;

    public abstract class Application : ApplicationCore
    {
        protected Application(IDependencyContainer container, bool owner = true) : base(container, owner)
        {
        }
    }
}