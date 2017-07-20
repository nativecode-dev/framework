namespace NativeCode.Core.DotNet.Platform.Security.Authentication
{
    using System;
    using System.Diagnostics;
    using System.DirectoryServices.AccountManagement;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Platform.Security;
    using Core.Platform.Security.Authentication;
    using Extensions;

    public class WindowsAuthenticationHandler : IAuthenticationHandler
    {
        public Task<AuthenticationResult> AuthenticateAsync(string login, string password,
            CancellationToken cancellationToken)
        {
            AuthenticationResultType result;
            var principal = ApplicationPrincipal.Anonymous;

            try
            {
                var adname = UserLoginName.Parse(login);

                using (var context = new PrincipalContext(ContextType.Domain, adname.Domain))
                {
                    using (var user = UserPrincipal.FindByIdentity(context, adname.Login))
                    {
                        result = AuthenticateUser(user, password);

                        if (result == AuthenticationResultType.Authenticated && user != null)
                            principal = new WindowsPrincipal(new WindowsIdentity(user.UserPrincipalName));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToExceptionString());
                result = AuthenticationResultType.Failed;
            }

            return Task.FromResult(new AuthenticationResult(result, principal));
        }

        public bool CanHandle(string login)
        {
            return UserLoginName.IsValid(login, UserLoginNameFormat.UserPrincipalName);
        }

        private static AuthenticationResultType AuthenticateUser(AuthenticablePrincipal user, string password)
        {
            if (user == null)
                return AuthenticationResultType.NotFound;

            if (user.IsAccountLockedOut())
                return AuthenticationResultType.Locked;

            if (user.AccountExpirationDate != null)
            {
                var expiration = new DateTimeOffset(user.AccountExpirationDate.Value);

                if (DateTimeOffset.UtcNow < expiration)
                    return AuthenticationResultType.Expired;
            }

            if (user.Context.ValidateCredentials(user.SamAccountName, password))
                return AuthenticationResultType.Authenticated;

            return AuthenticationResultType.Denied;
        }
    }
}