namespace Comp2007_Final.Migration.DataContext
{
    using Models;
    using System;
    using System.Collections.Generic;
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

            List<string> finishes = new List<string>();
            finishes.Add("Matte");
            finishes.Add("Glossy");

            foreach (var finish in finishes)
            {
                ItemFinish newFinish = new ItemFinish();
                newFinish.Name = finish;

                ItemFinish checkfinish = context.ItemFinishes.SingleOrDefault(x => x.Name == newFinish.Name);
                if (checkfinish == null)
                {
                    context.ItemFinishes.Add(newFinish);
                }
            }
            context.SaveChanges();

            
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
