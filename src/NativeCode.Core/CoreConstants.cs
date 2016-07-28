namespace NativeCode.Core
{
    public static class CoreConstants
    {
#if RELEASE
        public const string AssemblyVersion = "1.0.0-release";
#else
        public const string AssemblyVersion = "1.0.0-dev";
#endif

        public const string Company = "NativeCode Development";

        public const string Copyright = "Copyright © NativeCode Development 2016";
    }
}