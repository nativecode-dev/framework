namespace NativeCode.Core.Authorization
{
    using JetBrains.Annotations;
    using NativeCode.Core.Authorization.Exceptions;
    using NativeCode.Core.Extensions;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class SecurityStringParser : ISecurityStringParser
    {
        /// <remarks> a-z - match any alpha with case ignored () - parens used to represent sub-expressions ! - used to negate an assertion # - assert permission
        /// $ - assert feature @ - assert group/role, note names with spaces can use underscore & - AND operator | - OR operator </remarks>
        private static readonly Regex RegexLegalCharacters = new Regex(@"[a-z,\(,\),&,\|,#!,\$,@,\.,\*,_,\s]", RegexOptions.IgnoreCase);

        private static readonly ConcurrentDictionary<string, SimpleToken> Evaluations = new ConcurrentDictionary<string, SimpleToken>();

        private static readonly SimpleTokenType[] CanPrecedeConstant = { SimpleTokenType.Expression, SimpleTokenType.Modifier, SimpleTokenType.Operator };

        private static readonly SimpleTokenType[] CanPrecedeExpression = { SimpleTokenType.Expression, SimpleTokenType.Modifier, SimpleTokenType.Operator, SimpleTokenType.Root };

        private static readonly SimpleTokenType[] CanPrecedeLiterals = { SimpleTokenType.Constant, SimpleTokenType.Literal };

        private static readonly SimpleTokenType[] CanPrecedeModifier = { SimpleTokenType.Expression, SimpleTokenType.Operator, SimpleTokenType.Terminator };

        private static readonly SimpleTokenType[] CanPrecedeOperator = { SimpleTokenType.Expression, SimpleTokenType.Literal, SimpleTokenType.Terminator };

        private static readonly SimpleTokenType[] CanPrecedeSpace = { SimpleTokenType.Expression, SimpleTokenType.Literal, SimpleTokenType.Operator, SimpleTokenType.Terminator };

        private static readonly SimpleTokenType[] CanPrecedeTerminator = { SimpleTokenType.Expression, SimpleTokenType.Literal, SimpleTokenType.Terminator };

        private readonly ISecurityEvaluator evaluator;

        public SecurityStringParser(ISecurityEvaluator evaluator)
        {
            this.evaluator = evaluator;
        }

        public void Assert(string source, ISecurityEvaluatorContext context)
        {
            if (this.Evaluate(source, context) == false)
            {
                throw new AuthorizationAssertionFailedException(source);
            }
        }

        public bool Evaluate(string source, ISecurityEvaluatorContext context)
        {
            if (!source.StartsWith("("))
            {
                source = $"({source})";
            }

            return this.evaluator.Evaluate(Tokenize(source), context);
        }

        private static SimpleToken Tokenize([NotNull] string source)
        {
            if (Evaluations.ContainsKey(source))
            {
                return Evaluations[source];
            }

            var root = SimpleToken.Root();
            var current = root;
            var depth = 0;
            var position = -1;
            var errors = new List<string>();
            char? lastCharacter = null;

            foreach (var character in source)
            {
                position++;

                if (RegexLegalCharacters.IsMatch(character.ToString()) == false)
                {
                    continue;
                }

                Debug.WriteLine(character);

                switch (character)
                {
                    case '(':
                        if (CanPrecedeExpression.Contains(current.Type) == false)
                        {
                            errors.Add(
                                $"Sub-expressions '{character}' can only follow [{string.Join(",", CanPrecedeExpression)}], but like a sub, you came up with {current.Type}.");
                        }
                        depth++;
                        current = SimpleToken.Expression(current, position, depth);
                        break;

                    case ')':
                        if (CanPrecedeTerminator.Contains(current.Type) == false)
                        {
                            errors.Add(
                                $"Terminators '{character}' can only follow [{string.Join(",", CanPrecedeTerminator)}], except the T-1000 was a {current.Type}.");
                        }
                        depth--;
                        current = SimpleToken.Terminate(current, position);
                        break;

                    case '!':
                        if (CanPrecedeModifier.Contains(current.Type) == false)
                        {
                            errors.Add(
                                $"Modifiers '{character}' can only follow [{string.Join(",", CanPrecedeModifier)}], but you gon' dun' it with {current.Type}.");
                        }
                        current = SimpleToken.Modifier(current, position, character.ToString());
                        break;

                    case '*':
                    case '#':
                    case '$':
                        if (CanPrecedeConstant.Contains(current.Type) == false)
                        {
                            errors.Add(
                                $"Constants '{character}' can only follow [{string.Join(",", CanPrecedeConstant)}], but you keep insisting on a {current.Type}.");
                        }
                        current = SimpleToken.Constant(current, position, character.ToString());
                        break;

                    case '&':
                    case '|':
                        if (CanPrecedeOperator.Contains(current.Type) == false)
                        {
                            errors.Add(
                                $"Operators '{character}' can only follow [{string.Join(",", CanPrecedeOperator)}] because they come back and found {current.Type}.");
                        }
                        current = SimpleToken.Operator(current, position, character.ToString());
                        break;

                    case ' ':
                        if (CanPrecedeSpace.Contains(current.Type) == false)
                        {
                            errors.Add($"Spaces can only follow [{CanPrecedeSpace}], but threw it to {current.Type}.");
                        }
                        break;

                    default:
                        if (current.Type != SimpleTokenType.Literal)
                        {
                            if (CanPrecedeLiterals.Contains(current.Type) == false)
                            {
                                errors.Add(
                                    $"Literals '{character}' can only follow [{string.Join(",", CanPrecedeLiterals)}], but found {current.Type}. Literally.");
                            }

                            current = SimpleToken.Literal(current, position, character.ToString());
                        }

                        if (lastCharacter == ' ' && current.Type == SimpleTokenType.Literal)
                        {
                            errors.Add($"Spaces are not allowed in literals.");
                        }

                        current.Value += character.ToString();
                        break;
                }

                lastCharacter = character;
            }

            if (errors.Any())
            {
                throw new InvalidOperationException(string.Join(Environment.NewLine, errors));
            }

            if (Evaluations.TryAdd(source, root) == false)
            {
                Debug.WriteLine("Failed to cache token.");
            }

            return root;
        }
    }
}
