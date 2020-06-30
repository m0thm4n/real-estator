namespace RealEstator.Migrations
{
    using Microsoft.Owin.BuilderProperties;
    using RealEstator.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RealEstator.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RealEstator.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Condos.AddOrUpdate(x => x.CondoID,
                new Condo()
                {
                    CondoID = 1,
                    Address = "123 Test Ave",
                    Beds = 1,
                    Baths = 1,
                    SquareFootage = 500,
                    HasPool = false,
                    IsWaterfront = false,
                    Occupied = true,
                    YearBuilt = 1900,
                    Price = 100,
                });
        }
    }
}
