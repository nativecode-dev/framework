﻿namespace NativeCode.Core.Platform.Maintenance
{
    using System;

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class MaintenanceIgnoreAttribute : Attribute
    {
    }
}
