namespace NativeCode.Core.Authorization
{
    public enum TokenType
    {
        Literal = 0,

        Comment,

        Constant,

        Expression,

        Identifier,

        Keyword,

        NonPrintable,

        Symbol,

        Terminator,

        Whitespace
    }
}