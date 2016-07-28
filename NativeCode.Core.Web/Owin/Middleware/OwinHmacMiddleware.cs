namespace NativeCode.Core.Web.Owin.Middleware
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using NativeCode.Core.Dependencies;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Platform.Security;
    using NativeCode.Core.Web.Hmac;

    public class OwinHmacMiddleware : OwinMiddleware
    {
        public OwinHmacMiddleware(Func<IDictionary<string, object>, Task> next)
            : base(next)
        {
        }

        protected override Task PostInvokeAsync(IDictionary<string, object> environment)
        {
            return Task.FromResult(0);
        }

        protected override async Task PreInvokeAsync(IDictionary<string, object> environment)
        {
            var crypto = this.Request.Headers[HttpHeaders.KeyRequestSignatureCrypto];
            var length = this.Request.Headers[HttpHeaders.ContentLength];
            var signature = this.Request.Headers[HttpHeaders.KeyRequestSignature];
            var timestamp = this.Request.Headers[HttpHeaders.KeyRequestSignatureTimestamp];

            var provider = DependencyLocator.Resolver.Resolve<IHmacSettingsProvider>();
            var secret = await provider.GetUserSecretAsync(this.Request.User, this.Request.CallCancelled).ConfigureAwait(false);

            if (StringExtensions.NotEmpty(crypto, length, signature, timestamp))
            {
                var canonical = new CanonicalMessage(this.Request);
                canonical.Sign(crypto.ToEnum<HmacSignatureAlgorithm>(), secret);
            }
        }
    }
}