namespace Common.Migrations.KnownInstances
{
    using System;

    public static class KnownStorage
    {
        private static readonly Lazy<object> Initializer = new Lazy<object>(Initialize);

        private static object Initialize()
        {
            return new object();
        }
    }
}