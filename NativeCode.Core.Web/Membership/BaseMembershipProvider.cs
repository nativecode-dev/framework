namespace NativeCode.Core.Web.Membership
{
    using System;
    using System.Web.Security;

    using NativeCode.Core.DotNet.Providers;

    public abstract class BaseMembershipProvider : MembershipProvider
    {
        protected readonly WindowsAuthenticationProvider Provider = new WindowsAuthenticationProvider();

        public override string ApplicationName
        {
            get
            {
                return Global.Settings.GetValue("global.membership.application_name", default(string));
            }

            set
            {
                Global.Settings.SetValue("global.membership.application_name", value);
            }
        }

        public override bool EnablePasswordReset => Global.Settings.GetValue("global.membership.enable_password_reset", false);

        public override bool EnablePasswordRetrieval => Global.Settings.GetValue("global.membership.enable_password_retrieval", false);

        public override int MaxInvalidPasswordAttempts => Global.Settings.GetValue("global.membership.max_invalid_password_attempts", 5);

        public override int MinRequiredNonAlphanumericCharacters => Global.Settings.GetValue("global.minimum_required_non_alphanumeric_character", 0);

        public override int MinRequiredPasswordLength => Global.Settings.GetValue("global.membership.minimum_required_password_length", 6);

        public override int PasswordAttemptWindow => Global.Settings.GetValue("global.membership.password_attempt_window", 5);

        public override MembershipPasswordFormat PasswordFormat
            => Global.Settings.GetValue("global.membership.password_format", MembershipPasswordFormat.Encrypted);

        public override string PasswordStrengthRegularExpression
            => Global.Settings.GetValue("global.membership.password_strength_regular_expression", default(string));

        public override bool RequiresQuestionAndAnswer => Global.Settings.GetValue("global.membership.require_question_and_answer", false);

        public override bool RequiresUniqueEmail => Global.Settings.GetValue("global.membership.requires_unique_email", true);

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotSupportedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotSupportedException();
        }

        public override MembershipUser CreateUser(
            string username, 
            string password, 
            string email, 
            string passwordQuestion, 
            string passwordAnswer, 
            bool isApproved, 
            object providerUserKey, 
            out MembershipCreateStatus status)
        {
            throw new NotSupportedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotSupportedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotSupportedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotSupportedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotSupportedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotSupportedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotSupportedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotSupportedException();
        }
    }
}