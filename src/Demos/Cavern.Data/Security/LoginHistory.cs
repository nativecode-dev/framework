namespace Cavern.Data.Security
{
    using System;
    using NativeCode.Core.Data;
    using NativeCode.Core.Platform.Security.Authentication;

    public class LoginHistory : Entity<Guid>
    {
        public AuthenticationResultType AuthenticationResult { get; set; }

        public Login Login { get; set; }
    }
}
