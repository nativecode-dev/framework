namespace NativeCode.Core.Platform.Logging
{
    using System;
    using System.Collections.Generic;
    using Dependencies.Attributes;

    [IgnoreDependency("Use new operator.")]
    public class LogMessage
    {
        public DateTimeOffset Created { get; } = DateTimeOffset.UtcNow;

        public string Message { get; set; }

        public List<string> Tags { get; } = new List<string>();

        public LogMessageType Type { get; set; }
    }
}