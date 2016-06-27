namespace Common.Data
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using NativeCode.Packages.Data;

    public class CoreDataFactory : DbDataFactory<CoreDataContext>
    {
        public CoreDataFactory(CoreDataContext context)
            : base(context)
        {
        }

        public override void Seed<T>(IEnumerable<T> entities)
        {
            var dbset = this.Context.Set<T>();
            dbset.AddOrUpdate(entities.ToArray());
        }
    }
}