namespace Common.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Migration10001 : DbMigration
    {
        public override void Up()
        {
            this.AddColumn("dbo.Accounts", "Source", c => c.Int(false));
            this.CreateIndex("dbo.Accounts", new[] { "DomainName", "Login" }, true, "UX_Account");
        }

        public override void Down()
        {
            this.DropIndex("dbo.Accounts", "UX_Account");
            this.DropColumn("dbo.Accounts", "Source");
        }
    }
}