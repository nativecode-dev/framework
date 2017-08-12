namespace NativeCode.Core.Localization.Translation.Attributes
{
    using System;

    /// <summary>
    /// Marks a property to be ignored from the translation process.
    /// This class cannot be inherited.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class IgnoreTranslateAttribute : Attribute
    {
    }
}