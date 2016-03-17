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
                "dbo.Downloads",
                c =>
                new
                    {
                        Key = c.Guid(false),
                        Filename = c.String(false, 64),
                        ClaimMachineName = c.String(maxLength: 64),
                        Source = c.String(false, 1024),
                        State = c.Int(false),
                        Title = c.String(false, 512),
                        Url = c.String(false, 1024),
                        DateCreated = c.DateTimeOffset(precision: 7),
                        DateModified = c.DateTimeOffset(precision: 7),
                        UserCreated = c.String(),
                        UserModified = c.String(),
                        Account_Key = c.Long(),
                        Storage_Key = c.Int()
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.Accounts", t => t.Account_Key)
                .ForeignKey("dbo.Storages", t => t.Storage_Key)
                .Index(t => t.Account_Key)
                .Index(t => t.Storage_Key);

            this.CreateTable(
                "dbo.DownloadProperties",
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
                        Download_Key = c.Guid()
                    }).PrimaryKey(t => t.Key).ForeignKey("dbo.Downloads", t => t.Download_Key).Index(t => t.Download_Key);

            this.CreateTable(
                "dbo.Storages",
                c =>
                new
                    {
                        Key = c.Int(false, true),
                        MachineName = c.String(false, 64),
                        Name = c.String(false, 64),
                        Path = c.String(false, 1024),
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
                "dbo.MenuActions",
                c =>
                new
                    {
                        Key = c.Int(false, true),
                        Caption = c.String(false, 32),
                        Description = c.String(maxLength: 256),
                        Enabled = c.Boolean(false),
                        Type = c.Int(false),
                        Visible = c.Boolean(false),
                        DateCreated = c.DateTimeOffset(precision: 7),
                        DateModified = c.DateTimeOffset(precision: 7),
                        UserCreated = c.String(),
                        UserModified = c.String(),
                        ParentAction_Key = c.Int()
                    }).PrimaryKey(t => t.Key).ForeignKey("dbo.MenuActions", t => t.ParentAction_Key).Index(t => t.ParentAction_Key);

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

            this.CreateTable(
                "dbo.TokenProperties",
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
                        Token_Key = c.Guid()
                    }).PrimaryKey(t => t.Key).ForeignKey("dbo.Tokens", t => t.Token_Key).Index(t => t.Token_Key);
        }

        public override void Down()
        {
            this.DropForeignKey("dbo.TokenProperties", "Token_Key", "dbo.Tokens");
            this.DropForeignKey("dbo.Tokens", "Account_Key", "dbo.Accounts");
            this.DropForeignKey("dbo.MenuActions", "ParentAction_Key", "dbo.MenuActions");
            this.DropForeignKey("dbo.AccountProperties", "Account_Key", "dbo.Accounts");
            this.DropForeignKey("dbo.Downloads", "Storage_Key", "dbo.Storages");
            this.DropForeignKey("dbo.DownloadProperties", "Download_Key", "dbo.Downloads");
            this.DropForeignKey("dbo.Downloads", "Account_Key", "dbo.Accounts");
            this.DropIndex("dbo.TokenProperties", new[] { "Token_Key" });
            this.DropIndex("dbo.Tokens", new[] { "Account_Key" });
            this.DropIndex("dbo.MenuActions", new[] { "ParentAction_Key" });
            this.DropIndex("dbo.AccountProperties", new[] { "Account_Key" });
            this.DropIndex("dbo.DownloadProperties", new[] { "Download_Key" });
            this.DropIndex("dbo.Downloads", new[] { "Storage_Key" });
            this.DropIndex("dbo.Downloads", new[] { "Account_Key" });
            this.DropTable("dbo.TokenProperties");
            this.DropTable("dbo.Tokens");
            this.DropTable("dbo.MenuActions");
            this.DropTable("dbo.AccountProperties");
            this.DropTable("dbo.Storages");
            this.DropTable("dbo.DownloadProperties");
            this.DropTable("dbo.Downloads");
            this.DropTable("dbo.Accounts");
        }
    }
}