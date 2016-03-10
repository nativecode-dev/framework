namespace Services.Models.Downloads
{
    using NativeCode.Web.Models;

    public class QueueDownloadRequest : Request
    {
        public string Filename { get; set; }

        public string Path { get; set; }

        public string Url { get; set; }
    }
}