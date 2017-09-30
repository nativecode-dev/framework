namespace NativeCode.Core.Web.WebSockets.Commands
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Command
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public IDictionary<string, string> Parameters { get; protected set; } = new Dictionary<string, string>();
    }
}