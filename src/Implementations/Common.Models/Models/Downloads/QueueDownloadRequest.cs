namespace Common.Models.Models.Downloads
{
    using System.ComponentModel.DataAnnotations;

    public class QueueDownloadRequest : Request
    {
        [Required]
        public string Filename { get; set; }

        [Required]
        public string Storage { get; set; }

        [Required]
        public string Source { get; set; }

        public string Title { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string Url { get; set; }
    }
}