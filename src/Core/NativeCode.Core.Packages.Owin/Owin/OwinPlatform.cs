namespace NativeCode.Core.Packages.Owin.Owin
{
    using Dependencies;
    using DotNet.Platform;

    public class OwinPlatform : DotNetPlatform
    {
        public OwinPlatform(IDependencyContainer container)
            : base(container)
        {
        }
    }
}