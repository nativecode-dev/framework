namespace NativeCode.Core.Authorization
{
    using System;
    using System.Collections.Generic;

    public class SecurityEvaluator : ISecurityEvaluator
    {
        public bool Evaluate(SimpleToken token, ISecurityEvaluatorContext context)
        {
            foreach (var expression in this.Walk(token, x => x.Type == SimpleTokenType.Expression))
            {
            }

            return true;
        }

        private IEnumerable<SimpleToken> Walk(SimpleToken token, Predicate<SimpleToken> predicate)
        {
            foreach (var child in token.Children)
            {
                if (predicate(child))
                {
                    yield return child;
                }

                foreach (var grandchild in this.Walk(child, predicate))
                {
                    yield return grandchild;
                }
            }
        }
    }
}