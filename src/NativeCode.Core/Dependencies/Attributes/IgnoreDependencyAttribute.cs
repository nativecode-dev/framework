namespace NativeCode.Core.Dependencies.Attributes
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    using JetBrains.Annotations;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class IgnoreDependencyAttribute : Attribute
    {
        public IgnoreDependencyAttribute(string reason, bool inheritable = true, bool throwOnError = true)
        {
            this.Inheritable = inheritable;
            this.Reason = reason;
            this.ThrowOnError = throwOnError;
        }

        public bool Inheritable { get; }

        public string Reason { get; }

        public bool ThrowOnError { get; }

        public static bool ValidateType([ItemCanBeNull] Type type)
        {
            var attribute = type?.GetTypeInfo().GetCustomAttribute<IgnoreDependencyAttribute>(true);

            if (attribute != null)
            {
                Debug.WriteLine(attribute.Reason);

                if (attribute.Inheritable && attribute.ThrowOnError)
                {
                    throw new InvalidOperationException($"Registration for {type.Name} not allowed due to {attribute.Reason}.");
                }

                return false;
            }

            return true;
        }
    }
}