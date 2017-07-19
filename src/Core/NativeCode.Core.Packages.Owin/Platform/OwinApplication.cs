﻿namespace NativeCode.Core.Packages.Owin.Platform
{
    using System;
    using System.Security.Principal;
    using System.Threading;
    using Core.Platform;
    using Microsoft.Owin.Hosting;
    using Settings;

    public class OwinApplication : Application
    {
        public OwinApplication(IPlatform platform, Settings settings)
            : base(platform, settings)
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

        protected override void PreInitialization()
        {
            Thread.CurrentPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());

            base.PreInitialization();
        }
    }
}