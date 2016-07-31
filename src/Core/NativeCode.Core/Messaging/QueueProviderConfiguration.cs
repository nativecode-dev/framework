namespace NativeCode.Core.Messaging
{
    using System;

    public abstract class QueueProviderConfiguration
    {
        public Uri BaseAddress { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
