namespace NativeCode.Core.Localization.Translation.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class IgnoreTranslateAttribute : Attribute
    {
    }
}