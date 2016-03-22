namespace NativeCode.Core.Authorization
{
    using JetBrains.Annotations;

    public interface ISecurityStringParser
    {
        void Assert([NotNull] string source, [NotNull] ISecurityEvaluatorContext context);

        bool Evaluate([NotNull] string source, [NotNull] ISecurityEvaluatorContext context);
    }
}