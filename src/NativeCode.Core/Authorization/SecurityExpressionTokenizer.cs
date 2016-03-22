namespace NativeCode.Core.Authorization
{
    using System.IO;
    using System.Linq;
    using System.Text;

    using NativeCode.Core.Types.Structs;

    public class SecurityExpressionTokenizer
    {
        public SecurityExpressionTokenizer(string source)
        {
            this.Stream = new MemoryStream(Encoding.UTF8.GetBytes(source));
        }

        protected byte[] Buffer { get; } = new byte[4096];

        protected int BufferCount { get; private set; }

        protected int BufferPosition { get; private set; }

        protected Stream Stream { get; }

        public Token GetNextToken()
        {
            return null;
        }

        private char MoveNext()
        {
            var next = (char)this.Buffer[this.BufferPosition];
            this.BufferPosition++;

            if (this.BufferCount == this.BufferPosition)
            {
                this.FillBuffer();
            }

            return next;
        }

        private void FillBuffer()
        {
            this.BufferCount = this.Stream.Read(this.Buffer, 0, this.Buffer.Length);
        }
    }

    public class Token
    {
        public Position Position { get; set; }

        public TokenType Type { get; set; }

        public string Value { get; set; }
    }

    public enum TokenRule
    {
        Consume,

        Ignore,

        Return
    }

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