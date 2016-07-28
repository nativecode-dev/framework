namespace NativeCode.Core.Authorization
{
    public interface ISecurityEvaluator
    {
        bool Evaluate(SimpleToken token, ISecurityEvaluatorContext context);
    }
}