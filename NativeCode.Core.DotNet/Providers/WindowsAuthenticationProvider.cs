namespace NativeCode.Core.DotNet.Providers
{
    using System;
    using System.Diagnostics;
    using System.DirectoryServices.AccountManagement;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    using NativeCode.Core.Extensions;
    using NativeCode.Core.Platform.Security;

    using Principal = NativeCode.Core.Platform.Principal;

    public class WindowsAuthenticationProvider : IAuthenticationProvider
    {
        public bool CanHandle(string login)
        {
            return UserLoginName.IsValid(login, UserLoginNameFormat.UserPrincipalName);
        }

        public IPrincipal CreatePrincipal(string login)
        {
            return new WindowsPrincipal(new WindowsIdentity(login));
        }

        public Task<AuthenticationResult> AuthenticateAsync(string login, string password, CancellationToken cancellationToken)
        {
            AuthenticationResultType result;
            var principal = Principal.Anonymous;

            try
            {
                var adname = UserLoginName.Parse(login);

                using (var context = new PrincipalContext(ContextType.Domain, adname.Domain))
                {
                    using (var user = UserPrincipal.FindByIdentity(context, adname.Login))
                    {
                        result = AuthenticateUser(user, password);

                        if (result == AuthenticationResultType.Authenticated && user != null)
                        {
                            principal = this.CreatePrincipal(user.UserPrincipalName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Stringify());
                result = AuthenticationResultType.Failed;
            }

            return Task.FromResult(new AuthenticationResult(result, principal));
        }

        private static AuthenticationResultType AuthenticateUser(AuthenticablePrincipal user, string password)
        {
            if (user == null)
            {
                return AuthenticationResultType.NotFound;
            }

            if (user.IsAccountLockedOut())
            {
                return AuthenticationResultType.Locked;
            }

            if (user.AccountExpirationDate != null)
            {
                var expiration = new DateTimeOffset(user.AccountExpirationDate.Value);

                if (DateTimeOffset.UtcNow < expiration)
                {
                    return AuthenticationResultType.Expired;
                }
            }

            if (user.Context.ValidateCredentials(user.SamAccountName, password))
            {
                return AuthenticationResultType.Authenticated;
            }

            return AuthenticationResultType.Denied;
        }
    }
}