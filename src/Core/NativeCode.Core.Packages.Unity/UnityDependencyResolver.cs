namespace NativeCode.Core.Packages.Unity
{
    using System;
    using System.Collections.Generic;
    using Dependencies;
    using Dependencies.Exceptions;
    using Microsoft.Practices.Unity;

    public class UnityDependencyResolver : DependencyResolver
    {
        private readonly IUnityContainer container;

        public UnityDependencyResolver(IUnityContainer container)
        {
            this.container = container;
            this.container.RegisterInstance<IDependencyResolver>(this);
        }

        public override object Resolve(Type type, string key = null)
        {
#if DEBUG
            var typename = type.Name;
#endif

            try
            {
                return this.container.Resolve(type, key);
            }
            catch (ResolutionFailedException rfe)
            {
                throw new DependencyResolveException(type, rfe);
            }
        }

        public override IEnumerable<object> ResolveAll(Type type)
        {
            try
            {
                return this.container.ResolveAll(type);
            }
            catch (ResolutionFailedException rfe)
            {
                throw new DependencyResolveException(type, rfe);
            }
        }
    }
}