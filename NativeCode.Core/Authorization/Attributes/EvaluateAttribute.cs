namespace NativeCode.Core.Authorization.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class EvaluateAttribute : Attribute
    {
        public EvaluateAttribute(string requires, string handler = default(string), params object[] parameters)
        {
            this.Handler = handler;
            this.Parameters = parameters;
            this.RequireString = requires;
        }

        public string Handler { get; }

        public object[] Parameters { get; }

        public string RequireString { get; }
    }
}