namespace Common.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Common.Data.Entities.Enums;

    using NativeCode.Core.Data;

    public class Download : Entity<Guid>
    {
        public Account Account { get; set; }

        [Required]
        [MaxLength(64)]
        public string Filename { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Path { get; set; }

        public DownloadState State { get; set; }

        [Required]
        [MaxLength(512)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Url { get; set; }
    }
}