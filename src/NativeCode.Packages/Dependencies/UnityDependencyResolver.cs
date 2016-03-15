namespace NativeCode.Packages.Dependencies
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Practices.Unity;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Dependencies.Exceptions;

    public class UnityDependencyResolver : DependencyResolver
    {
        private readonly IUnityContainer container;

        public UnityDependencyResolver(IUnityContainer container)
        {
            this.container = container;
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