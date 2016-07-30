namespace NativeCode.Core.Validation
{
    using System;

    [Flags]
    public enum StringComplexityRules
    {
        None = 0, 

        RequireAlpha = 1, 

        RequireCasing = 1 << 1, 

        RequireNumeric = 1 << 2, 

        RequireSymbols = 1 << 3, 

        RequireAll = RequireAlpha | RequireCasing | RequireNumeric | RequireSymbols
    }
}