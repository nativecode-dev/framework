namespace Common.Web
{
    using System;
    using System.IO;

    using Humanizer;

    using NativeCode.Core;
    using NativeCode.Core.DotNet;
    using NativeCode.Core.Web;
    using NativeCode.Core.Web.Owin;
    using NativeCode.Packages.Dependencies;
    using NativeCode.Packages.Platform;

    public class ServicesApplication : OwinApplication
    {
        private const string DefaultSettingsFile = "settings.json";

        public ServicesApplication()
            : base(new OwinPlatform(new UnityDependencyContainer()))
        {
            this.Initialize(
                this.GetType().Name.Humanize(),
                CoreDependencies.Instance,
                DotNetDependencies.Instance,
                WebDependencies.Instance,
                CommonDependencies.Instance,
                CommonWebDependencies.Instance);
        }

        protected override void PostInitialization()
        {
            using (var container = this.Platform.CreateDependencyScope())
            {
                Database.Configure(container.Resolver);
            }

            base.PostInitialization();
        }

        protected override void PersistSettings()
        {
            base.PersistSettings();

            var filename = Path.Combine(Environment.CurrentDirectory, DefaultSettingsFile);

            using (var filestream = File.Open(filename, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                this.Settings.Save(filestream);
            }
        }

        protected override void RestoreSettings()
        {
            base.RestoreSettings();

            var filename = Path.Combine(Environment.CurrentDirectory, DefaultSettingsFile);

            if (File.Exists(filename))
            {
                using (var filestream = File.OpenRead(filename))
                {
                    this.Settings.Load(filestream);
                }
            }
        }
    }
}