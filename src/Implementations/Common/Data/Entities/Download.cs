namespace Common.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common.Models.Models.Enums;

    using NativeCode.Core.Data;

    public class Download : Entity<Guid>
    {
        public Account Account { get; set; }

        [Required]
        [MaxLength(64)]
        public string Filename { get; set; }

        [MaxLength(64)]
        public string ClaimMachineName { get; set; }

        public List<DownloadProperty> Properties { get; set; }

        [Required]
        [DataType(DataType.Url)]
        [MaxLength(1024)]
        public string Source { get; set; }

        public DownloadState State { get; set; }

        public Storage Storage { get; set; }

        [Required]
        [MaxLength(512)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Url)]
        [MaxLength(1024)]
        public string Url { get; set; }
    }
}