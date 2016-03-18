namespace NativeCode.Core.Web.Owin
{
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.DotNet.Platform;

    public class OwinPlatform : DotNetPlatform
    {
        public OwinPlatform(IDependencyContainer container)
            : base(container)
        {
        }
    }
}