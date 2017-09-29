namespace NativeCode.Core.Platform.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text.RegularExpressions;
    using Dependencies;
    using JetBrains.Annotations;

    public class UserLoginName
    {
        private const string PatternDomain =
            @"^(?<name>[a-z][a-z0-9\.-]+)\\(?<domain>(?![\x20\.]+$)([^\\/""[\\]:|<>+=;,?\*@]+))$";

        private const string PatternName = @"^(?<name>[a-z][a-z0-9\.-]+)";

        private const string PatternUserPrincipalName = @"^(?<name>[A-Z0-9._%+-]+)@(?<domain>[A-Z0-9.-]+\.[A-Z]{2,})$";

        private static readonly Dictionary<UserLoginNameFormat, Regex> Formats =
            new Dictionary<UserLoginNameFormat, Regex>();

        static UserLoginName()
        {
            UserLoginName.Formats.Add(UserLoginNameFormat.Domain, new Regex(UserLoginName.PatternDomain, RegexOptions.IgnoreCase));
            UserLoginName.Formats.Add(UserLoginNameFormat.Name, new Regex(UserLoginName.PatternName, RegexOptions.IgnoreCase));
            UserLoginName.Formats.Add(UserLoginNameFormat.UserPrincipalName,
                new Regex(UserLoginName.PatternUserPrincipalName, RegexOptions.IgnoreCase));
        }

        private UserLoginName([NotNull] string source, [NotNull] string domain, [NotNull] string login,
            UserLoginNameFormat format)
        {
            this.Domain = domain;
            this.Format = format;
            this.Login = login;
            this.Source = source;
        }

        public string Domain { get; }

        public UserLoginNameFormat Format { get; }

        public string Login { get; }

        protected string Source { get; }

        public static UserLoginNameFormat GetLoginNameFormat([NotNull] string username)
        {
            return (from kvp in UserLoginName.Formats where kvp.Value.IsMatch(username) select kvp.Key).FirstOrDefault();
        }

        public static bool IsValid([NotNull] string source)
        {
            return UserLoginName.Formats.Values.Any(x => x.IsMatch(source));
        }

        public static bool IsValid([NotNull] string source, UserLoginNameFormat format)
        {
            var formatter = UserLoginName.Formats[format];
            return formatter.IsMatch(source);
        }

        public static bool IsValid([NotNull] IPrincipal principal)
        {
            return UserLoginName.IsValid(principal.Identity);
        }

        public static bool IsValid([NotNull] IIdentity identity)
        {
            return UserLoginName.IsValid(identity.Name);
        }

        public static UserLoginName Parse([NotNull] string source)
        {
            UserLoginName userLoginName;

            if (UserLoginName.TryParse(source, out userLoginName))
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

            var formatter = UserLoginName.GetFormatter(source);
            var format = formatter.Key;
            var regex = formatter.Value;

            userLoginName = UserLoginName.GetUserLoginName(source, regex, format);

            return userLoginName != null;
        }

        private static KeyValuePair<UserLoginNameFormat, Regex> GetFormatter([NotNull] string source)
        {
            foreach (var kvp in UserLoginName.Formats)
            {
                var formatter = kvp.Value;

                if (formatter.IsMatch(source))
                {
                    return kvp;
                }
            }

            throw new InvalidOperationException($"No formatters could be found to match {source}.");
        }

        private static UserLoginName GetUserLoginName([NotNull] string source, [NotNull] Regex regex,
            UserLoginNameFormat format)
        {
            var match = regex.Match(source);

            if (match != null)
            {
                var domain = match.Groups["domain"].Value;
                var login = match.Groups["name"].Value;

                if (string.IsNullOrWhiteSpace(domain))
                {
                    using (var scope = DependencyLocator.GetContainer())
                    using (var resolver = scope.CreateResolver())
                    {
                        var application = resolver.Resolve<IApplication>();
                        domain = application.SettingsObject.GetValue<string>("Global.DefaultDomain");
                    }
                }

                return new UserLoginName(source, domain, login, format);
            }

            throw new InvalidOperationException($"Failed to create UserLoginName instance from {source}.");
        }
    }
}