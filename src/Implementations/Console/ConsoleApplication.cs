namespace Console
{
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Platform;

    internal class ConsoleApplication : Application
    {
        public ConsoleApplication(IDependencyContainer container, bool owner = true) : base(container, owner)
        {
        }
    }
}