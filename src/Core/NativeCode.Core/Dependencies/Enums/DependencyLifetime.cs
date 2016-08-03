namespace NativeCode.Core.Dependencies.Enums
{
    public enum DependencyLifetime
    {
        /// <summary>
        /// Indicates to use the default lifetime.
        /// </summary>
        Default = 0,

        /// <summary>
        /// Indicates that resolved dependencies exist for the lifetime of the application.
        /// </summary>
        PerApplication = 1,

        /// <summary>
        /// Indicates that each resolve will create a new instance.
        /// </summary>
        PerCall = Default,

        /// <summary>
        /// Indicates that each child container will create its own instance.
        /// </summary>
        PerContainer = 2,

        /// <summary>
        /// Indicates that during the course of a resolve, when multiple contructors require the
        /// same type, a single instance is used.
        /// </summary>
        PerResolve = 3,

        /// <summary>
        /// Indicates that the resolved instance lives on the current thread.
        /// </summary>
        PerThread = 4
    }
}