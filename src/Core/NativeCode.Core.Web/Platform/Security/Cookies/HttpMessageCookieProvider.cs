namespace NativeCode.Core.Web.Platform.Security.Cookies
{
    using System.Net.Http;
    using System.Web;

    using NativeCode.Core.Serialization;

    public class HttpMessageCookieProvider<TData> : CookieProvider<HttpRequestMessage, HttpResponseMessage, HttpCookie, TData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpMessageCookieProvider{TData}" /> class.
        /// </summary>
        /// <param name="serializer">The serializer.</param>
        public HttpMessageCookieProvider(IStringSerializer serializer)
            : base(serializer)
        {
        }

        public override HttpCookie CreateCookie(string name, TData data, bool persist = true, int timeout = 20, int version = 1)
        {
            return new HttpCookie(name, this.Serializer.Serialize(data));
        }

        public override HttpCookie GetCookie(HttpRequestMessage request, string name)
        {
            // TODO: Suspicious code?
            var cookie = request.GetRequestCookie();

            if (string.IsNullOrWhiteSpace(cookie) == false)
            {
                return new HttpCookie(name, cookie);
            }

            return new HttpCookie(name);
        }

        public override TData GetCookieData(HttpCookie cookie)
        {
            return this.Serializer.Deserialize<TData>(cookie.Value);
        }

        public override void SetCookie(HttpResponseMessage response, HttpCookie cookie)
        {
            response.SetResponseCookie(cookie);
        }
    }
}