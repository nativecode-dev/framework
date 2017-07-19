namespace NativeCode.Core.Web.Platform.Security.Authentication
{
    using Core.Platform;
    using Dependencies;
    using Settings;

    public class CookieAuthenticationData : JsonSettings
    {
        private const string KeyCustomIdentifier = "NativeCode.Web.Authentication.CustomIdentifier";

        private const string KeyDomainName = "NativeCode.Web.Authentication.DomainName";

        private const string KeyLoginName = "NativeCode.Web.Authentication.LoginName";

        private static readonly string MachineName = DependencyLocator.Resolver.Resolve<IPlatform>().MachineName;

        internal CookieAuthenticationData()
        {
        }

        public string CustomIdentifier
        {
            get => this.GetValue<string>(KeyCustomIdentifier);

            set => this.SetValue(KeyCustomIdentifier, value);
        }

        public string DomainName
        {
            get => this.GetValue(KeyDomainName, MachineName);

            set => this.SetValue(KeyDomainName, value);
        }

        public string LoginName
        {
            get => this.GetValue<string>(KeyLoginName);

            set => this.SetValue(KeyLoginName, value);
        }
    }
}