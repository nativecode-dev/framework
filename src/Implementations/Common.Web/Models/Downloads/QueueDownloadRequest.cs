namespace Common.Web.Models.Downloads
{
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Web.Models;

    public class QueueDownloadRequest : Request
    {
        [Required]
        public string Filename { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public string Source { get; set; }

        public string Title { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string Url { get; set; }
    }
}