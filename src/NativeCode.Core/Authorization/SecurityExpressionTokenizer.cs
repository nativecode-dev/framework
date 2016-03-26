namespace NativeCode.Core.Authorization
{
    using System.IO;
    using System.Text;

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
}