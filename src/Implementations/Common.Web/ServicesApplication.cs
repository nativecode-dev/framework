namespace Common.Web
{
    using System;
    using System.IO;

    using Humanizer;

    using NativeCode.Core;
    using NativeCode.Core.DotNet;
    using NativeCode.Core.Platform;
    using NativeCode.Packages.Dependencies;
    using NativeCode.Packages.Platform;
    using NativeCode.Web;

    public class ServicesApplication : OwinApplication
    {
        private const string DefaultSettingsFile = "settings.json";

        public ServicesApplication()
            : base(new UnityDependencyContainer(), true)
        {
            Current = this;

            this.Initialize(
                this.GetType().Name.Humanize(),
                CoreDependencies.Instance,
                DotNetDependencies.Instance,
                WebDependencies.Instance,
                CommonDependencies.Instance,
                CommonWebDependencies.Instance);
        }

        public static IApplication Current { get; private set; }

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