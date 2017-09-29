namespace NativeCode.Core.Dependencies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Types;

    public abstract class DependencyResolver : Disposable, IDependencyResolver
    {
        public T Resolve<T>(string key = null)
        {
            return (T) this.Resolve(typeof(T), key);
        }

        public abstract IDependencyResolver CreateChildResolver();

        public abstract object Resolve(Type type, string key = null);

        public IEnumerable<T> ResolveAll<T>()
        {
            return this.ResolveAll(typeof(T)).Cast<T>();
        }

        public abstract IEnumerable<object> ResolveAll(Type type);

        object IServiceProvider.GetService(Type serviceType)
        {
            return this.Resolve(serviceType);
        }
    }
}