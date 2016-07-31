namespace NativeCode.Core.Web.Platform.Providers.Cookies
{
    using System.Web;

    using NativeCode.Core.Serialization;

    public class HttpContextCookieProvider<TData> : CookieProvider<HttpRequest, HttpResponse, HttpCookie, TData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpContextCookieProvider{TData}" /> class.
        /// </summary>
        /// <param name="serializer">The serializer.</param>
        public HttpContextCookieProvider(IStringSerializer serializer)
            : base(serializer)
        {
        }

        public override HttpCookie CreateCookie(string name, TData data, bool persist = true, int timeout = 20, int version = 1)
        {
            return new HttpCookie(name, this.Serializer.Serialize(data));
        }

        public override HttpCookie GetCookie(HttpRequest request, string name)
        {
            return request.Cookies[name];
        }

        public override TData GetCookieData(HttpCookie cookie)
        {
            return this.Serializer.Deserialize<TData>(cookie.Value);
        }

        public override void SetCookie(HttpResponse response, HttpCookie cookie)
        {
            response.Cookies.Set(cookie);
        }
    }
}