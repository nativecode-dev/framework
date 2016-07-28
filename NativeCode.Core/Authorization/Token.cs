namespace NativeCode.Core.Authorization
{
    using NativeCode.Core.Types.Structs;

    public class Token
    {
        public Position Position { get; set; }

        public TokenType Type { get; set; }

        public string Value { get; set; }
    }
}