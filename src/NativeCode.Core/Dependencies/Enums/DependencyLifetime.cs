namespace NativeCode.Core.Dependencies.Enums
{
    public enum DependencyLifetime
    {
        Default = 0,

        PerApplication = 1,

        PerCall = Default,

        PerContainer = 2,

        PerResolve = 3,

        PerThread = 4
    }
}