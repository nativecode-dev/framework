namespace NativeCode.Core.Web
{
    using System.Linq;
    using System.Net.Http;

    public static class HttpHeaders
    {
        #region Headers

        public const string Accept = "Accept";

        public const string Authorization = "Authorization";

        public const string ContentHash = "Content-MD5";

        public const string ContentLength = "Content-Length";

        public const string ContentType = "Content-Type";

        public const string SetCookie = "Set-Cookie";

        #endregion

        #region Keys

        public const string KeyRequestUserKey = "X-Custom-Request-UserKey";

        public const string KeyRequestSignature = "X-Custom-Request-Signature";

        public const string KeyRequestSignatureCrypto = "X-Custom-Request-Signature-Crypto";

        public const string KeyRequestSignatureTimestamp = "X-Custom-Request-Signature-Timestamp";

        #endregion

        public static string GetHeaderValue(this HttpRequestMessage request, string key, string defaultValue = default(string))
        {
            if (request.Headers.Contains(key))
            {
                return request.Headers.GetValues(key).FirstOrDefault();
            }

            return defaultValue;
        }

        public static string GetRequestKey(this HttpRequestMessage request)
        {
            return request.GetHeaderValue(KeyRequestUserKey);
        }

        public static string GetRequestSignature(this HttpRequestMessage request)
        {
            return request.GetHeaderValue(KeyRequestSignature);
        }

        public static string GetRequestSignatureCrypto(this HttpRequestMessage request)
        {
            return request.GetHeaderValue(KeyRequestSignatureCrypto);
        }

        public static string GetRequestCookie(this HttpRequestMessage request)
        {
            return request.GetHeaderValue(SetCookie);
        }
    }
}