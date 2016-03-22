namespace NativeCode.Core.Authorization
{
    using System;
    using System.Collections.Generic;

    public class SimpleToken
    {
        private SimpleToken(SimpleTokenType type, SimpleToken parent, int position, string value = default(string), int depth = default(int))
        {
            this.Depth = depth;
            this.Parent = parent;
            this.Position = position;
            this.Type = type;
            this.Value = value;

            this.Parent?.Children.Add(this);
        }

        public List<SimpleToken> Children { get; } = new List<SimpleToken>();

        public int Depth { get; }

        public SimpleToken Parent { get; }

        public int Position { get; }

        public SimpleTokenType Type { get; }

        public string Value { get; set; }

        public static SimpleToken Constant(SimpleToken parent, int position, string value)
        {
            return new SimpleToken(SimpleTokenType.Constant, parent, position, value);
        }

        public static SimpleToken Expression(SimpleToken parent, int position, int depth)
        {
            return new SimpleToken(SimpleTokenType.Expression, parent, position, depth: depth);
        }

        public static SimpleToken Literal(SimpleToken parent, int position, string value)
        {
            return new SimpleToken(SimpleTokenType.Literal, parent, position, value);
        }

        public static SimpleToken Modifier(SimpleToken parent, int position, string value)
        {
            return new SimpleToken(SimpleTokenType.Modifier, parent, position, value);
        }

        public static SimpleToken Operator(SimpleToken parent, int position, string value)
        {
            return new SimpleToken(SimpleTokenType.Operator, parent, position, value);
        }

        public static SimpleToken Root()
        {
            return new SimpleToken(SimpleTokenType.Root, null, -1);
        }

        public static SimpleToken Terminate(SimpleToken parent, int position)
        {
            return new SimpleToken(SimpleTokenType.Terminator, parent, position);
        }

        public IEnumerable<SimpleToken> FindConstants()
        {
            return this.Find(this, x => x.Type == SimpleTokenType.Constant);
        }

        public IEnumerable<SimpleToken> FindExpressions()
        {
            return this.Find(this, x => x.Type == SimpleTokenType.Expression);
        }

        private IEnumerable<SimpleToken> Find(SimpleToken token, Func<SimpleToken, bool> filter)
        {
            var tokens = new List<SimpleToken>();

            if (filter(token))
            {
                tokens.Add(token);
            }

            foreach (var child in token.Children)
            {
                tokens.AddRange(this.Find(child, filter));
            }

            return tokens;
        }
    }
}