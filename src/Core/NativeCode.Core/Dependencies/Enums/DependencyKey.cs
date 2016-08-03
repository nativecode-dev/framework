namespace NativeCode.Core.Dependencies.Enums
{
    public enum DependencyKey
    {
        /// <summary>
        /// Indicates not to use a key.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates to use the type full name.
        /// </summary>
        Name = 1,

        /// <summary>
        /// Indicates to use the fully-qualified type name.
        /// </summary>
        QualifiedName = 2,

        /// <summary>
        /// Indicates to use the type name with no namespace.
        /// </summary>
        ShortName = 3
    }
}