namespace NativeCode.Core.Platform.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text.RegularExpressions;

    using JetBrains.Annotations;

    using NativeCode.Core.Dependencies;

    public class UserLoginName
    {
        private static readonly Dictionary<UserLoginNameFormat, Regex> Formats = new Dictionary<UserLoginNameFormat, Regex>();

        private const string PatternDomain = @"^(?<name>[a-z][a-z0-9\.-]+)\\(?<domain>(?![\x20\.]+$)([^\\/""[\\]:|<>+=;,?\*@]+))$";

        private const string PatternName = @"^(?<name>[a-z][a-z0-9\.-]+)";

        private const string PatternUserPrincipalName = @"^(?<name>[A-Z0-9._%+-]+)@(?<domain>[A-Z0-9.-]+\.[A-Z]{2,})$";

        static UserLoginName()
        {
            Formats.Add(UserLoginNameFormat.Domain, new Regex(PatternDomain, RegexOptions.IgnoreCase));
            Formats.Add(UserLoginNameFormat.Name, new Regex(PatternName, RegexOptions.IgnoreCase));
            Formats.Add(UserLoginNameFormat.UserPrincipalName, new Regex(PatternUserPrincipalName, RegexOptions.IgnoreCase));
        }

        private UserLoginName([NotNull] string source, [NotNull] string domain, [NotNull] string login, UserLoginNameFormat format)
        {
            this.Domain = domain;
            this.Format = format;
            this.Login = login;
            this.Source = source;
        }

        public string Domain { get; }

        public string Login { get; }

        public UserLoginNameFormat Format { get; private set; }

        protected string Source { get; }

        public static UserLoginNameFormat GetLoginNameFormat([NotNull] string username)
        {
            return (from kvp in Formats where kvp.Value.IsMatch(username) select kvp.Key).FirstOrDefault();
        }

        public static bool IsValid([NotNull] string source)
        {
            return Formats.Values.Any(x => x.IsMatch(source));
        }

        public static bool IsValid([NotNull] string source, UserLoginNameFormat format)
        {
            var formatter = Formats[format];
            return formatter.IsMatch(source);
        }

        public static bool IsValid([NotNull] IPrincipal principal)
        {
            return IsValid(principal.Identity);
        }

        public static bool IsValid([NotNull] IIdentity identity)
        {
            return IsValid(identity.Name);
        }

        public static UserLoginName Parse([NotNull] string source)
        {
            UserLoginName userLoginName;

            if (TryParse(source, out userLoginName))
            {
                return userLoginName;
            }

            throw new InvalidOperationException($"Could not parse source '{source}' to determine the format.");
        }

        public static bool TryParse([NotNull] string source, out UserLoginName userLoginName)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                userLoginName = null;
                return false;
            }

            var formatter = GetFormatter(source);
            var format = formatter.Key;
            var regex = formatter.Value;

            userLoginName = GetUserLoginName(source, regex, format);

            return userLoginName != null;
        }

        private static KeyValuePair<UserLoginNameFormat, Regex> GetFormatter([NotNull] string source)
        {
            foreach (var kvp in Formats)
            {
                var formatter = kvp.Value;

                if (formatter.IsMatch(source))
                {
                    return kvp;
                }
            }

            throw new InvalidOperationException($"No formatters could be found to match {source}.");
        }

        private static UserLoginName GetUserLoginName([NotNull] string source, [NotNull] Regex regex, UserLoginNameFormat format)
        {
            var match = regex.Match(source);

            if (match != null)
            {
                var domain = match.Groups["domain"].Value;
                var login = match.Groups["name"].Value;

                if (string.IsNullOrWhiteSpace(domain))
                {
                    var application = DependencyLocator.Resolver.Resolve<IApplication>();
                    domain = application.Settings.GetValue<string>("Global.DefaultDomain");
                }

                return new UserLoginName(source, domain, login, format);
            }

            throw new InvalidOperationException($"Failed to create UserLoginName instance from {source}.");
        }
    }
}