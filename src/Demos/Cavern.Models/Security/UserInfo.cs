namespace Cavern.Models.Security
{
    using Common;

    public class UserInfo : BasicInfo
    {
        public string DisplayName { get; set; }

        public string Email { get; set; }

        public bool Enabled { get; set; }

        public LoginType LoginType { get; set; }
    }
}
