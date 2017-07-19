namespace NativeCode.Core.Web
{
    using System.Linq;
    using System.Net.Http;
    using System.Web;

    public static class HttpHeaders
    {
        public const string Accept = "Accept";

        public const string ApiKey = "X-Custom-API-Key";

        public const string ApiKeyId = "X-Custom-API-Id";

        public const string Authorization = "Authorization";

        public const string ContentHash = "Content-MD5";

        public const string ContentLength = "Content-Length";

        public const string ContentType = "Content-Type";

        public const string KeyRequestSignature = "X-Custom-Request-Signature";

        public const string KeyRequestSignatureCrypto = "X-Custom-Request-Signature-Crypto";

        public const string KeyRequestSignatureTimestamp = "X-Custom-Request-Signature-Timestamp";

        public const string KeyRequestUserKey = "X-Custom-Request-UserKey";

        public const string SetCookie = "Set-Cookie";

        public static string GetApiKey(this HttpRequestMessage request)
        {
            return request.GetHeaderValue(ApiKey);
        }

        public static string GetApiKeyId(this HttpRequestMessage request)
        {
            return request.GetHeaderValue(ApiKeyId);
        }

        public static string GetAuthorization(this HttpRequestMessage request)
        {
            return request.GetHeaderValue(Authorization);
        }

        public static string GetContentHash(this HttpRequestMessage request)
        {
            return request.GetHeaderValue(ContentHash);
        }

        public static int GetContentLength(this HttpRequestMessage request)
        {
            int length;

            if (int.TryParse(request.GetHeaderValue(ContentLength), out length))
                return length;

            return default(int);
        }

        public static string GetContentType(this HttpRequestMessage request)
        {
            return request.GetHeaderValue(ContentType);
        }

        public static string GetHeaderValue(this HttpRequestMessage request, string key,
            string defaultValue = default(string))
        {
            if (request.Headers.Contains(key))
                return request.Headers.GetValues(key).FirstOrDefault();

            return defaultValue;
        }

        public static string GetRequestCookie(this HttpRequestMessage request)
        {
            return request.GetHeaderValue(SetCookie);
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

        public static void SetResponseCookie(this HttpResponseMessage response, HttpCookie cookie)
        {
            response.Headers.TryAddWithoutValidation(SetCookie, cookie.Value);
        }
    }
}