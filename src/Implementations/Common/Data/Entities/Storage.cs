namespace Common.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class Storage : Entity<int>
    {
        [Required]
        [MaxLength(64)]
        public string MachineName { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Path { get; set; }
    }
}