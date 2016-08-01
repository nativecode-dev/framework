namespace NativeCode.Core.Validation
{
    using System;

    [Flags]
    public enum StringComplexityRules
    {
        /// <summary>
        /// Indicates that no complexity validation is required.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates that the string must contain characters.
        /// </summary>
        RequireAlpha = 1,

        /// <summary>
        /// Indicates that the string must contain mixed-cases.
        /// </summary>
        RequireCasing = 1 << 1,

        /// <summary>
        /// Indicates that the string must contain numbers.
        /// </summary>
        RequireNumeric = 1 << 2,

        /// <summary>
        /// Indicates that the string must contain symbols.
        /// </summary>
        RequireSymbols = 1 << 3,

        /// <summary>
        /// Indicates that all of the requirements should be used.
        /// </summary>
        RequireAll = RequireAlpha | RequireCasing | RequireNumeric | RequireSymbols
    }
}