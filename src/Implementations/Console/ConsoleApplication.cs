namespace Console
{
    using NativeCode.Core;
    using NativeCode.Core.DotNet;
    using NativeCode.Core.Platform;

    internal class ConsoleApplication : Application
    {
        public ConsoleApplication(IPlatform platform)
            : base(platform)
        {
            this.Initialize("Console Application", CoreDependencies.Instance, DotNetDependencies.Instance);
        }
    }
}