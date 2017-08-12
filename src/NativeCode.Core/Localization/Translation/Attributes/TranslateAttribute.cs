namespace NativeCode.Core.Localization.Translation.Attributes
{
    using System;

    /// <summary>
    /// Marks a class as requiring translation of the string properties.
    /// This class cannot be inherited.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public sealed class TranslateAttribute : Attribute
    {
    }
}