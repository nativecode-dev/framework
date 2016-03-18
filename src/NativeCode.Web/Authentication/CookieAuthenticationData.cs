namespace NativeCode.Core.Web.Authentication
{
    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Settings;

    public class CookieAuthenticationData : JsonSettings
    {
        private const string KeyCustomIdentifier = "NativeCode.Web.Authentication.CustomIdentifier";

        private const string KeyDomainName = "NativeCode.Web.Authentication.DomainName";

        private const string KeyLoginName = "NativeCode.Web.Authentication.LoginName";

        private static readonly string MachineName = DependencyLocator.Resolver.Resolve<IPlatform>().MachineName;

        internal CookieAuthenticationData()
        {
        }

        public string DomainName
        {
            get
            {
                return this.GetValue(KeyDomainName, MachineName);
            }

            set
            {
                this.SetValue(KeyDomainName, value);
            }
        }

        public string LoginName
        {
            get
            {
                return this.GetValue<string>(KeyLoginName);
            }

            set
            {
                this.SetValue(KeyLoginName, value);
            }
        }

        public string CustomIdentifier
        {
            get
            {
                return this.GetValue<string>(KeyCustomIdentifier);
            }

            set
            {
                this.SetValue(KeyCustomIdentifier, value);
            }
        }
    }
}