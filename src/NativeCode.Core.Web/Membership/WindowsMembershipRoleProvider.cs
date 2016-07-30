namespace NativeCode.Core.Web.Membership
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.DirectoryServices.AccountManagement;
    using System.Linq;
    using System.Web.Configuration;
    using System.Web.Security;

    using NativeCode.Core.Platform.Security;

    public class WindowsMembershipRoleProvider : WindowsTokenRoleProvider
    {
        private const string PathRoleManager = "system.web/roleManager";

        private static readonly Lazy<RoleManagerSection> MembershipProviderSettingsInstance = new Lazy<RoleManagerSection>(LoadRoleManagerSection);

        public static RoleManagerSection RoleManagerSection => MembershipProviderSettingsInstance.Value;

        public override string[] GetRolesForUser(string username)
        {
            var domain = GetDomainFromSettings();

            if (UserLoginName.IsValid(username, UserLoginNameFormat.UserPrincipalName) == false)
            {
                return GetGroupsForUser(domain, username).ToArray();
            }

            return base.GetRolesForUser(username);
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return false;
        }

        private static string GetDomainFromSettings()
        {
            return RoleManagerSection.Domain;
        }

        private static IEnumerable<string> GetGroupsForUser(string domain, string username)
        {
            var result = new List<string>();

            using (var principal = new PrincipalContext(ContextType.Domain, domain))
            {
                using (var user = UserPrincipal.FindByIdentity(principal, username))
                {
                    if (user == null)
                    {
                        return result;
                    }

                    using (var groups = user.GetAuthorizationGroups())
                    {
                        result.AddRange(groups.OfType<GroupPrincipal>().Select(item => item.Name));
                    }
                }
            }

            return result;
        }

        private static RoleManagerSection LoadRoleManagerSection()
        {
            return (RoleManagerSection)ConfigurationManager.GetSection(PathRoleManager);
        }
    }
}