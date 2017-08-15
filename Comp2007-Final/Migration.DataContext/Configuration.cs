namespace Comp2007_Final.Migration.DataContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Comp2007_Final.Models.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migration.DataContext";
        }

        protected override void Seed(Comp2007_Final.Models.DataContext context)
        {
            context.Colours.Add(new Models.Colour { Name = "Blue" });
            context.Colours.Add(new Models.Colour { Name = "Green" });
            context.Colours.Add(new Models.Colour { Name = "Orange" });
            context.Colours.Add(new Models.Colour { Name = "Purple" });


            context.ItemFinishes.Add(new Models.ItemFinish { Name = "Matte" });
            context.ItemFinishes.Add(new Models.ItemFinish { Name = "Glossy" });
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
