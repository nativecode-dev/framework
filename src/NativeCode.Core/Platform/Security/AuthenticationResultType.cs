namespace NativeCode.Core.Platform.Security
{
    public enum AuthenticationResultType
    {
        Default = 0,

        Authenticated = Default,

        Denied = 1,

        Expired = 2,

        Failed = 3,

        Locked = 4,

        Marked = 5,

        NotFound = 6
    }
}