namespace NativeCode.Core.Platform
{
    public abstract class Application : ApplicationProxy
    {
        protected Application(IPlatform platform)
            : base(platform)
        {
        }
    }
}