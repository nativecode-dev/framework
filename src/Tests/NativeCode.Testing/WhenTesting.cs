namespace NativeCode.Testing
{
    using System;
    using System.IO;
    using System.Reflection;
    using Core.Types;
    using JetBrains.Annotations;

    public abstract class WhenTesting : DisposableManager
    {
        [CanBeNull]
        protected virtual Stream ManifestStream([NotNull] string name)
        {
            return this.ManifestStream(name, this.GetType().Assembly);
        }

        [CanBeNull]
        protected virtual Stream ManifestStream([NotNull] string name, [NotNull] Assembly assembly)
        {
            return assembly.GetManifestResourceStream(name);
        }

        [NotNull]
        protected virtual string ManifestString([NotNull] string name)
        {
            return this.ManifestString(name, this.GetType().Assembly);
        }

        [NotNull]
        protected virtual string ManifestString([NotNull] string name, [NotNull] Assembly assembly)
        {
            var stream = this.ManifestStream(name, assembly);

            if (stream == null)
                throw new InvalidOperationException("Could not get stream to read from.");

            using (var reader = new StreamReader(stream, false))
            {
                return reader.ReadToEnd();
            }
        }
    }
}