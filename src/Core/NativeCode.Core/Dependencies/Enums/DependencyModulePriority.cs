namespace NativeCode.Core.Dependencies.Enums
{
    /// <summary>
    /// Enumeration of dependency module priorities.
    /// </summary>
    public enum DependencyModulePriority
    {
        /// <summary>
        /// Indicates that no particular priority is required.
        /// </summary>
        Any = 0,

        /// <summary>
        /// Indicates that the priority is optional.
        /// </summary>
        Optional = 1,

        /// <summary>
        /// Indicates that the priority is important.
        /// </summary>
        Important = 2
    }
}