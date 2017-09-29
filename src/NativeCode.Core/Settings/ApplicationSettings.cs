namespace NativeCode.Core.Settings
{
    using System;
    using Platform.Serialization;

    public class ApplicationSettings : JsonSettings
    {
        public ApplicationSettings(IStringSerializer serializer) : base(serializer)
        {
        }

        public string DefaultDomain
        {
            get => this.GetMemberValue(Environment.MachineName);
            set => this.SetMemberValue(value);
        }

        public string MachinName => this.GetMemberValue(Environment.MachineName);
    }
}