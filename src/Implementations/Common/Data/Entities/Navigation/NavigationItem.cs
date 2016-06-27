namespace Common.Data.Entities.Navigation
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using NativeCode.Core.Data;

    public class NavigationItem : Entity<int>
    {
        [MaxLength(CoreAnnotations.TextString)]
        public string Description { get; set; }

        [Required]
        [MaxLength(CoreAnnotations.TextName)]
        public string Name { get; set; }

        [Index]
        public NavigationItem Parent { get; set; }
    }
}