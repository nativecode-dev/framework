namespace NativeCode.Core.Platform.Maintenance
{
    using System;

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class MaintenanceImmuneAttribute : Attribute
    {
    }
}
