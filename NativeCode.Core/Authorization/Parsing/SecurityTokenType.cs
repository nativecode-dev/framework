namespace NativeCode.Core.Authorization.Parsing
{
    public enum SecurityTokenType
    {
        Default = 0, 

        Constant, 

        Identifier, 

        Literal = Default, 

        Operator, 

        Symbol
    }
}