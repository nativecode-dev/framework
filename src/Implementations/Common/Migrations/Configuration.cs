namespace Common.Migrations
{
    using System.Data.Entity.Migrations;

    using Common.Data;

    internal sealed class Configuration : DbMigrationsConfiguration<CoreDataContext>
    {
        public Configuration()
        {
#if DEBUG
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
#else
            this.AutomaticMigrationDataLossAllowed = false;
            this.AutomaticMigrationsEnabled = false;
#endif
        }

        protected override void Seed(CoreDataContext context)
        {
        }
    }
}