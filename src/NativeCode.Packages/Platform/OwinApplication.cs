namespace NativeCode.Packages.Platform
{
    using System;

    using Microsoft.Owin.Hosting;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Platform;

    public class OwinApplication : Application
    {
        public OwinApplication(IDependencyContainer container, bool owner = true)
            : base(container, owner)
        {
        }

        public IDisposable Start<T>(Uri url) where T : OwinStartup, new()
        {
            return this.Start<T>(url.AbsoluteUri);
        }

        public IDisposable Start<T>(string url) where T : OwinStartup, new()
        {
            return WebApp.Start<T>(new StartOptions(url));
        }

        public IDisposable Start<T>(StartOptions options) where T : OwinStartup, new()
        {
            this.EnsureInitialized();

            return WebApp.Start<T>(options);
        }
    }
}