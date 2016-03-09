namespace NativeCode.Web.AspNet.WebApi
{
    using System.Linq;
    using System.Net.Http;

    public static class HttpHeaders
    {
        public const string KeyRequestKey = "X-Custom-Request-Key";

        public const string KeyRequestSignature = "X-Custom-Request-Signature";

        public const string KeyRequestSignatureCrypto = "X-Custom-Request-Signature-Crypto";

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
            return request.GetHeaderValue(KeyRequestKey);
        }

        public static string GetRequestSignature(this HttpRequestMessage request)
        {
            return request.GetHeaderValue(KeyRequestSignature);
        }

        public static string GetRequestSignatureCrypto(this HttpRequestMessage request)
        {
            return request.GetHeaderValue(KeyRequestSignatureCrypto);
        }
    }
}