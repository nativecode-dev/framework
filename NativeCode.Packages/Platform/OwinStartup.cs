namespace NativeCode.Packages.Platform
{
    using Owin;

    public abstract class OwinStartup
    {
        public abstract void Configuration(IAppBuilder builder);
    }
}