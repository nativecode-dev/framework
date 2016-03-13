namespace NativeCode.Web.AspNet.WebApi.Formatters
{
    using System;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;

    using Newtonsoft.Json;

    public class JsonBrowserMediaTypeFormatter : JsonMediaTypeFormatter
    {
        public JsonBrowserMediaTypeFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            this.SerializerSettings.Formatting = Formatting.Indented;
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }
}