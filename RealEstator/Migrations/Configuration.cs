using Microsoft.Owin.BuilderProperties;
using RealEstator.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace RealEstator.Migrations
{

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

            context.Homes.AddOrUpdate(x => x.HomeID,
                new Home()
                {
                    HomeID = 1,
                    Address = "123 Test Ave",
                    Beds = 2,
                    Baths = 2,
                    SquareFootage = 1050,
                    HasPool = true,
                    IsWaterfront = false,
                    Occupied = false,
                    YearBuilt = 1990,
                    Price = 500,
                });

            context.Townhouses.AddOrUpdate(x => x.TownhouseID,
                new Townhouse()
                {
                    TownhouseID = 1,
                    Address = "123 Test Ave",
                    Beds = 2,
                    Baths = 1,
                    SquareFootage = 860,
                    HasPool = true,
                    IsWaterfront = true,
                    Occupied = true,
                    YearBuilt = 2001,
                    Price = 1050,
                });
        }
    }
}
