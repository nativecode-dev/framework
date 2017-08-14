namespace NativeCode.Core.Localization.Translation
{
    using System.Collections.Generic;
    using Dependencies.Attributes;
    using JetBrains.Annotations;

    [IgnoreDependency("Use new operator.")]
    public class ObjectTranslatorContext
    {
        private readonly HashSet<ObjectTranslatorContext> mappings;

        public ObjectTranslatorContext([NotNull] object instance)
        {
            this.mappings = new HashSet<ObjectTranslatorContext>();
            this.InstanceObject = instance;
        }

        public virtual object InstanceObject { get; }
    }

    [IgnoreDependency("Use new operator.")]
    public class ObjectTranslatorContext<T> : ObjectTranslatorContext
    {
        public ObjectTranslatorContext([NotNull] T instance) : base(instance)
        {
            this.Instance = instance;
        }

        public T Instance { get; }

        public override object InstanceObject => this.Instance;
    }
}