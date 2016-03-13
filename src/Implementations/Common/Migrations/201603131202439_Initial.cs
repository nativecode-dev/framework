namespace Common.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.Accounts",
                c =>
                new
                    {
                        Key = c.Long(false, true),
                        DomainHost = c.String(maxLength: 128),
                        DomainName = c.String(maxLength: 128),
                        Login = c.String(maxLength: 256),
                        Password = c.String(maxLength: 32),
                        DateCreated = c.DateTimeOffset(precision: 7),
                        DateModified = c.DateTimeOffset(precision: 7),
                        UserCreated = c.String(),
                        UserModified = c.String()
                    }).PrimaryKey(t => t.Key);

            this.CreateTable(
                "dbo.AccountProperties",
                c =>
                new
                    {
                        Key = c.Long(false, true),
                        Name = c.String(false, 64),
                        Value = c.String(),
                        DateCreated = c.DateTimeOffset(precision: 7),
                        DateModified = c.DateTimeOffset(precision: 7),
                        UserCreated = c.String(),
                        UserModified = c.String(),
                        Account_Key = c.Long()
                    }).PrimaryKey(t => t.Key).ForeignKey("dbo.Accounts", t => t.Account_Key).Index(t => t.Account_Key);

            this.CreateTable(
                "dbo.Downloads",
                c =>
                new
                    {
                        Key = c.Guid(false),
                        Filename = c.String(false, 64),
                        MachineName = c.String(maxLength: 64),
                        Path = c.String(false, 1024),
                        Source = c.String(false, 1024),
                        State = c.Int(false),
                        Title = c.String(false, 512),
                        Url = c.String(false, 1024),
                        DateCreated = c.DateTimeOffset(precision: 7),
                        DateModified = c.DateTimeOffset(precision: 7),
                        UserCreated = c.String(),
                        UserModified = c.String(),
                        Account_Key = c.Long()
                    }).PrimaryKey(t => t.Key).ForeignKey("dbo.Accounts", t => t.Account_Key).Index(t => t.Account_Key);

            this.CreateTable(
                "dbo.Tokens",
                c =>
                new
                    {
                        Key = c.Guid(false),
                        ExpirationDate = c.DateTimeOffset(precision: 7),
                        DateCreated = c.DateTimeOffset(precision: 7),
                        DateModified = c.DateTimeOffset(precision: 7),
                        UserCreated = c.String(),
                        UserModified = c.String(),
                        Account_Key = c.Long()
                    }).PrimaryKey(t => t.Key).ForeignKey("dbo.Accounts", t => t.Account_Key).Index(t => t.Account_Key);
        }

        public override void Down()
        {
            this.DropForeignKey("dbo.Tokens", "Account_Key", "dbo.Accounts");
            this.DropForeignKey("dbo.Downloads", "Account_Key", "dbo.Accounts");
            this.DropForeignKey("dbo.AccountProperties", "Account_Key", "dbo.Accounts");
            this.DropIndex("dbo.Tokens", new[] { "Account_Key" });
            this.DropIndex("dbo.Downloads", new[] { "Account_Key" });
            this.DropIndex("dbo.AccountProperties", new[] { "Account_Key" });
            this.DropTable("dbo.Tokens");
            this.DropTable("dbo.Downloads");
            this.DropTable("dbo.AccountProperties");
            this.DropTable("dbo.Accounts");
        }
    }
}