namespace NativeCode.Core.Authorization
{
    /// <summary>
    /// Provides a contract to evaluate a security token.
    /// </summary>
    public interface ISecurityEvaluator
    {
        bool IsValid(SecurityToken token, ISecurityEvaluatorContext context);
    }
}