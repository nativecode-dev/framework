namespace Common.Models.Models
{
    using System;

    using Common.Models.Models.Enums;

    public class DownloadModel
    {
        public DateTimeOffset DateCreated { get; set; }

        public string Filename { get; set; }

        public string Path { get; set; }

        public string Source { get; set; }

        public DownloadState State { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }
    }
}