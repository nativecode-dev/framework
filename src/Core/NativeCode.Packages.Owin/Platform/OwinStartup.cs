namespace NativeCode.Core.Packages.Owin.Platform
{
    using global::Owin;

    public abstract class OwinStartup
    {
        public abstract void Configuration(IAppBuilder builder);
    }
}