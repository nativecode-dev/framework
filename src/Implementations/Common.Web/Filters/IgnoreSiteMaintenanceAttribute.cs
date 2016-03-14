namespace Common.Web.Filters
{
    using System;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class IgnoreSiteMaintenanceAttribute : Attribute
    {
    }
}