namespace SmartOnlineFurniture.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SmartOnlineFurniture.Models;
    internal sealed class Configuration : DbMigrationsConfiguration<SmartOnlineFurniture.DataContext.FurnitureDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SmartOnlineFurniture.DataContext.FurnitureDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

      

            //context.Woods.AddOrUpdate(
            //    new Woods() { Name="Chapalish"},
            //    new Woods() { WoodName = "Chapalish" },
            //    new Woods() { WodName = "Chapalish" },
            //    new Woods() { WoodName = "Chapalish" },
            //    new Woods() { WoodName = "Chapalish" },
            //    new Woods() { WoodName = "Chapalish" },
            //    new Woods() { WoodName = "Chapalish" },
            //    new Woods() { WoodName = "Chapalish" },
            //    new Woods() { WoodName = "Chapalish" },
            //    new Woods() { WoodName = "Chapalish" },
            //    new Woods() { WoodName = "Chapalish" }

            //    );
            //context.Woods.AddOrUpdate(

            //    new Woods() { WoodName="Chapalish"},
            //     new Woods() { WoodName = "Chapalish" },
            //      new Woods() { WoodName = "Chapalish" }

            //    );
        }
    }
}
