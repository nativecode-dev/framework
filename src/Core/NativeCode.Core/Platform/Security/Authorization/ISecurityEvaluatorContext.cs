namespace NativeCode.Core.Platform.Security.Authorization
{
    public interface ISecurityEvaluatorContext
    {
        bool HasFeature(string name);

        bool HasPermission(string name);

        bool HasRole(string name);
    }
}