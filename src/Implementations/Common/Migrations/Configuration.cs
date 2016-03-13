namespace Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.DirectoryServices;
    using System.DirectoryServices.AccountManagement;
    using System.Linq;
    using System.Security.Principal;
    using System.Threading;

    using Common.Data;
    using Common.Data.Entities;

    using NativeCode.Core.Extensions;
    using NativeCode.Core.Types;

    internal sealed class Configuration : DbMigrationsConfiguration<CoreDataContext>
    {
        public Configuration()
        {
#if DEBUG
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
#else
            this.AutomaticMigrationDataLossAllowed = false;
            this.AutomaticMigrationsEnabled = false;
#endif
        }

        protected override void Seed(CoreDataContext context)
        {
            var identity = WindowsIdentity.GetCurrent();
            var previous = Thread.CurrentPrincipal;

            using (new DisposableAction(() => Thread.CurrentPrincipal = previous))
            {
                if (identity == null || identity.IsAuthenticated.Not())
                {
                    throw new InvalidOperationException("Could not determine current windows identity.");
                }

                Thread.CurrentPrincipal = new WindowsPrincipal(identity);

                using (var principal = new PrincipalContext(System.DirectoryServices.AccountManagement.ContextType.Domain, Environment.UserDomainName))
                {
                    using (var searcher = new PrincipalSearcher(new UserPrincipal(principal)))
                    {
                        foreach (var result in searcher.FindAll())
                        {
                            var entry = (DirectoryEntry)result.GetUnderlyingObject();

                            var dn = (string)entry.Properties["distinguishedName"].Value;
                            var sam = (string)entry.Properties["sAMAccountName"].Value;
                            var upn = (string)entry.Properties["userPrincipalName"].Value;

                            if (StringExtensions.IsEmpty(dn, sam, upn).Not())
                            {
                                var parts = ParseDistinguishedName(dn);
                                var domain = string.Join(".", parts);
                                var account = new Account { DomainHost = domain, DomainName = Environment.UserDomainName, Login = sam };
                                account.Properties.Add(new AccountProperty { Name = "UserPrincipalName", Value = upn });

                                context.Accounts.AddOrUpdate(x => x.Login, account);
                            }
                        }
                    }
                }

                context.Save();
            }
        }

        private static string[] ParseDistinguishedName(string distinguishedName)
        {
            return distinguishedName.Split(',').Where(x => x.StartsWith("DC")).Select(x => x.Split('=')[1]).ToArray();
        }
    }
}