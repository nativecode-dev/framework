namespace NativeCode.Core.Authorization.Parsing
{
    using NativeCode.Core.Extensions;

    public class SecurityTokenizer
    {
        private static readonly char[] Symbols = { '@', '#', '$' };

        protected int Index { get; private set; }

        protected string Statement { get; private set; }

        public void Tokenize(string statement)
        {
            this.Index = 0;
            this.Statement = statement.TrimNewLines();
        }

        public bool Next(out char value)
        {
            var index = this.Index++;

            if (index < this.Statement.Length - 1)
            {
                value = this.Statement[index];
                return true;
            }

            value = char.MinValue;
            return false;
        }

        public bool Peek(out char value)
        {
            var index = this.Index + 1;

            if (index < this.Statement.Length - 1)
            {
                value = this.Statement[index];
                return true;
            }

            value = char.MinValue;
            return false;
        }

        public enum State
        {
            None = 0
        }
    }
}