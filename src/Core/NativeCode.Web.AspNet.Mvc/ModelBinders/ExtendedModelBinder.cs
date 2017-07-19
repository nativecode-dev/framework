namespace NativeCode.Web.AspNet.Mvc.ModelBinders
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;

    public class ExtendedModelBinder : DefaultModelBinder
    {
        private static readonly MethodInfo ToArrayMethod = typeof(Enumerable).GetMethod("ToArray");

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return this.BindCommaSeparatedString(bindingContext.ModelType, bindingContext.ModelName, bindingContext)
                   ?? base.BindModel(controllerContext, bindingContext);
        }

        protected override object GetPropertyValue(
            ControllerContext controllerContext,
            ModelBindingContext bindingContext,
            PropertyDescriptor propertyDescriptor,
            IModelBinder propertyBinder)
        {
            return this.BindCommaSeparatedString(propertyDescriptor.PropertyType, propertyDescriptor.Name,
                       bindingContext)
                   ?? base.GetPropertyValue(controllerContext, bindingContext, propertyDescriptor, propertyBinder);
        }

        private object BindCommaSeparatedString(Type type, string name, ModelBindingContext bindingContext)
        {
            if (type.GetInterface(typeof(IEnumerable).Name) == null)
                return null;

            var actual = bindingContext.ValueProvider.GetValue(name);

            if (actual == null)
                return null;

            var valueType = type.GetElementType() ?? type.GetGenericArguments().FirstOrDefault();

            if (valueType?.GetInterface(typeof(IConvertible).Name) == null)
                return null;

            var list = (IList) Activator.CreateInstance(typeof(List<>).MakeGenericType(valueType));

            foreach (var splitValue in actual.AttemptedValue.Split(','))
                if (!string.IsNullOrWhiteSpace(splitValue))
                    list.Add(Convert.ChangeType(splitValue, valueType));

            if (type.IsArray)
                return ToArrayMethod.MakeGenericMethod(valueType).Invoke(this, new object[] { list });

            return list;
        }
    }
}