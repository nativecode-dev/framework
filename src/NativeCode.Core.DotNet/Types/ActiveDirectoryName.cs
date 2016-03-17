namespace NativeCode.Core.DotNet.Types
{
    using System;
    using System.Security.Principal;

    using JetBrains.Annotations;

    public class ActiveDirectoryName
    {
        private ActiveDirectoryName([NotNull] string domain, [NotNull] string account, ActiveDirectoryNameFormat format)
        {
            this.Account = account;
            this.Domain = domain;
            this.Format = format;
        }

        public string Account { get; }

        public string Domain { get; }

        public ActiveDirectoryNameFormat Format { get; }

        public static string GetDomain(IPrincipal principal)
        {
            return GetDomain(principal.Identity);
        }

        public static string GetDomain(IIdentity identity)
        {
            var adn = Parse(identity.Name);

            return adn.Domain;
        }

        public static bool IsDnsName([NotNull] string login)
        {
            return login.Contains("@");
        }

        public static string GetLogin(IPrincipal principal)
        {
            return GetDomain(principal.Identity);
        }

        public static string GetLogin(IIdentity identity)
        {
            var adn = Parse(identity.Name);

            return adn.Account;
        }

        public static bool IsNtName([NotNull] string login)
        {
            return login.Contains("\\");
        }

        public static bool IsValid([NotNull] IPrincipal principal)
        {
            return IsValid(principal.Identity);
        }

        public static bool IsValid([NotNull] IIdentity identity)
        {
            return IsValid(identity.Name);
        }

        public static bool IsValid([NotNull] string login)
        {
            return IsDnsName(login) || IsNtName(login);
        }

        public static ActiveDirectoryName Parse([NotNull] string login)
        {
            var account = Create(login);

            if (account != null)
            {
                return account;
            }

            throw new InvalidOperationException("Format appears to be incorrect.");
        }

        public static bool TryParse([NotNull] string login, out ActiveDirectoryName account)
        {
            account = Create(login);

            return account != null;
        }

        public string ToString(ActiveDirectoryNameFormat format)
        {
            var separator = GetSeparator(format);

            switch (format)
            {
                case ActiveDirectoryNameFormat.DomainDnsName:
                    return $"{this.Account}{separator}{this.Domain}";

                default:
                    return $"{this.Domain}{separator}{this.Account}";
            }
        }

        public override string ToString()
        {
            return this.ToString(this.Format);
        }

        public string ToUpn()
        {
            return this.ToString(ActiveDirectoryNameFormat.DomainDnsName);
        }

        private static ActiveDirectoryName Create([NotNull] string login)
        {
            var format = GetFormat(login);
            var separator = GetSeparator(format);

            var parts = login.Split(separator);

            if (parts.Length == 2)
            {
                return Create(parts, format);
            }

            return null;
        }

        private static ActiveDirectoryName Create([NotNull] string[] parts, ActiveDirectoryNameFormat format)
        {
            switch (format)
            {
                case ActiveDirectoryNameFormat.DistinguishedName:
                    return new ActiveDirectoryName(parts[0], parts[1], format);

                case ActiveDirectoryNameFormat.DomainDnsName:
                    return new ActiveDirectoryName(parts[1], parts[0], format);

                default:
                    return new ActiveDirectoryName(parts[0], parts[1], format);
            }
        }

        private static ActiveDirectoryNameFormat GetFormat([NotNull] string login)
        {
            if (HasSeparator(login, ActiveDirectoryNameFormat.DistinguishedName))
            {
                return ActiveDirectoryNameFormat.DistinguishedName;
            }

            if (HasSeparator(login, ActiveDirectoryNameFormat.DomainDnsName))
            {
                return ActiveDirectoryNameFormat.DomainDnsName;
            }

            return ActiveDirectoryNameFormat.DomainName;
        }

        private static char GetSeparator(ActiveDirectoryNameFormat format)
        {
            switch (format)
            {
                case ActiveDirectoryNameFormat.DistinguishedName:
                    return ',';

                case ActiveDirectoryNameFormat.DomainDnsName:
                    return '@';

                default:
                    return '\\';
            }
        }

        private static bool HasSeparator([NotNull] string login, ActiveDirectoryNameFormat format)
        {
            return login.Contains(GetSeparator(format).ToString());
        }
    }
}