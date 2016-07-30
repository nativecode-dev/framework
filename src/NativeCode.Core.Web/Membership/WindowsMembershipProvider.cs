namespace NativeCode.Core.Web.Membership
{
    using System.Threading;

    using NativeCode.Core.Platform.Security;
    using NativeCode.Core.Platform.Security.Authentication;

    public class WindowsMembershipProvider : BaseMembershipProvider
    {
        public override bool ValidateUser(string username, string password)
        {
            if (this.Provider.CanHandle(username))
            {
                var response = this.Provider.AuthenticateAsync(username, password, CancellationToken.None).Result;

                if (response != null && response.Result == AuthenticationResultType.Authenticated)
                {
                    return true;
                }
            }

            return false;
        }
    }
}