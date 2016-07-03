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
    using Common.Data.Entities.Security;
    using Common.Data.Entities.Storage;

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
            ConfigureBaseData(context);

            context.Save();
        }

        private static void ConfigureBaseData(CoreDataContext context)
        {
            SetupActiveDirectoryAccounts(context);
            SetupStorage(context);
        }

        private static void SetupActiveDirectoryAccounts(CoreDataContext context)
        {
            var identity = WindowsIdentity.GetCurrent();
            var previous = Thread.CurrentPrincipal;

            using (new DisposableAction(() => Thread.CurrentPrincipal = previous))
            {
                if (identity == null || identity.IsAuthenticated == false)
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

                            if (StringExtensions.AllEmpty(dn, sam, upn) == false)
                            {
                                var parts = ParseDistinguishedName(dn);
                                var domain = string.Join(".", parts);

                                var account = new Account
                                                  {
                                                      DomainHost = domain,
                                                      DomainName = Environment.UserDomainName,
                                                      Login = sam,
                                                      Source = AccountSource.ActiveDirectory
                                                  };

                                account.Properties.Add(new AccountProperty { Name = "UserPrincipalName", Value = upn });

                                context.Accounts.AddOrUpdate(x => x.Login, account);
                            }
                        }
                    }
                }
            }
        }

        private static void SetupStorage(CoreDataContext context)
        {
            context.Storage.AddOrUpdate(
                x => x.Name,
                new Storage { Name = "Porn", MachineName = "STORAGE", Path = @"\\storage.nativecode.local\Download\Automatic" });
        }

        private static string[] ParseDistinguishedName(string distinguishedName)
        {
            return distinguishedName.Split(',').Where(x => x.StartsWith("DC")).Select(x => x.Split('=')[1]).ToArray();
        }
    }
}