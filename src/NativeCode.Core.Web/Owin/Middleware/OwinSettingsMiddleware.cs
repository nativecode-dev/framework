namespace NativeCode.Core.Web.Owin.Middleware
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.Owin;

    using NativeCode.Core.Settings;

    /// <summary>
    /// Creates a <see cref="JsonSettings" /> instance and stores it in the KeyRequestSettings location
    /// for a single request.
    /// </summary>
    public class OwinSettingsMiddleware : OwinMiddleware
    {
        public const string KeyRequestSettings = "Request.Settings";

        public OwinSettingsMiddleware(Func<IDictionary<string, object>, Task> next)
            : base(next)
        {
        }

        protected override Task PostInvokeAsync(IDictionary<string, object> environment)
        {
            var context = new OwinContext(environment);
            context.Request.Set(KeyRequestSettings, new JsonSettings());
            return Task.FromResult(0);
        }

        protected override Task PreInvokeAsync(IDictionary<string, object> environment)
        {
            return Task.FromResult(0);
        }
    }
}