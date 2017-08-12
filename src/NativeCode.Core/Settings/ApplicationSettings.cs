namespace NativeCode.Core.Settings
{
    using System;

    public class ApplicationSettings : JsonSettings
    {
        public string DefaultDomain
        {
            get => this.GetMemberValue(Environment.MachineName);
            set => this.SetMemberValue(value);
        }

        public string MachinName => this.GetMemberValue(Environment.MachineName);
    }
}