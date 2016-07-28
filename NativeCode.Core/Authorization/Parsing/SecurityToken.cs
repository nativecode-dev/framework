namespace NativeCode.Core.Authorization.Parsing
{
    public struct SecurityToken
    {
        public int Position { get; set; }

        public SecurityTokenType Type { get; set; }

        public string Value { get; set; }
    }
}