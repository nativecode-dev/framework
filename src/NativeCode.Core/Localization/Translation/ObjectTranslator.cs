﻿namespace NativeCode.Core.Localization.Translation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Attributes;
    using Caching;
    using Extensions;

    /// <summary>
    /// Translates an object's string properties if it contains translation tokens.
    /// </summary>
    /// <seealso cref="NativeCode.Core.Localization.Translation.IObjectTranslator" />
    public class ObjectTranslator : IObjectTranslator
    {
        private static readonly SafeCache<ObjectTranslatorMapping> Cache = new SafeCache<ObjectTranslatorMapping>();

        public ObjectTranslator(ITranslator translator)
        {
            this.Translator = translator;
        }

        protected ITranslator Translator { get; }

        public void Translate(object instance)
        {
            this.Translate(instance, new ObjectTranslatorContext());
        }

        public void Translate<T>(T instance)
        {
            this.Translate((object) instance);
        }

        protected virtual ObjectTranslatorMapping GetMapping(object instance)
        {
            return Cache.Get(instance.TypeKey(), () => new ObjectTranslatorMapping(instance));
        }

        protected void Translate(object instance, ObjectTranslatorContext context)
        {
            if (context.Resolved(instance) == false)
                context.Mark(instance);

            var mapping = this.GetMapping(instance);

            if (mapping.TranslatableProperties.Any())
                foreach (var property in mapping.TranslatableProperties)
                    this.TranslateProperty(property, context);
        }

        protected virtual void TranslateProperty(PropertyInfo property, ObjectTranslatorContext context)
        {
            var type = property.PropertyType;

            if (type == typeof(string))
                this.Translator.TranslateString((string) property.GetValue(context));
        }

        protected class ObjectTranslatorContext
        {
            private readonly HashSet<object> references = new HashSet<object>();

            public void Mark(object instance)
            {
                if (this.references.Contains(instance) == false)
                    this.references.Add(instance);
            }

            public bool Resolved(object instance)
            {
                return this.references.Contains(instance);
            }
        }

        protected class ObjectTranslatorMapping
        {
            public ObjectTranslatorMapping(object instance)
            {
                this.Type = instance.GetType();
                this.TranslatableProperties = this.Type.GetTranslatableProperties();
                this.TypeInfo = this.Type.GetTypeInfo();
            }

            public IEnumerable<PropertyInfo> TranslatableProperties;

            protected Type Type { get; }

            protected TypeInfo TypeInfo { get; }
        }
    }
}