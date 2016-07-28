namespace NativeCode.Core.Platform
{
    public abstract class Application : ApplicationCore
    {
        protected Application(IPlatform platform)
            : base(platform)
        {
        }
    }
}