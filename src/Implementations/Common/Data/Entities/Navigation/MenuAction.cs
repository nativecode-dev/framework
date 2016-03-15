namespace Common.Data.Entities.Navigation
{
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class MenuAction : Entity<int>
    {
        public MenuAction ParentAction { get; set; }

        [Required]
        [MaxLength(32)]
        public string Caption { get; set; }

        [MaxLength(256)]
        public string Description { get; set; }

        public bool Enabled { get; set; } = true;

        public MenuActionType Type { get; set; }

        public bool Visible { get; set; } = true;
    }
}