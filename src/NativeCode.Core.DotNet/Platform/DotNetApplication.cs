namespace NativeCode.Core.DotNet.Platform
{
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Platform;

    public abstract class DotNetApplication : Application
    {
        protected DotNetApplication(IDependencyContainer container, bool owner = true)
            : base(container, owner)
        {
        }
    }
}