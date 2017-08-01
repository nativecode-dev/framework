namespace Cavern.Models.Security
{
    using System;

    public abstract class BasicInfo
    {
        public DateTimeOffset DateCreated { get; set; }

        public DateTimeOffset DateModified { get; set; }

        public string UserCreated { get; set; }

        public string UserModified { get; set; }
    }
}