namespace Residence.DataLayer.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Residence.DataLayer.ResidenceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Residence.DataLayer.ResidenceContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            if (!context.Houses.Any())
            {
                var houses = new List<Housing>
                {
                    new Housing { HousingType = HousingType.House, Surface = 120, NoOfRooms = 6,
                        NoOfFlats = 2, HouseNo = 32, Description = "Lovely new house in the suburbs.",
                        Comodities = new List<Comodity>{ new Comodity { Description = "Hi-tech AC", Price = 1300, Currency = Currency.EUR} } },
                    new Housing { HousingType = HousingType.Apartment, Surface = 100, NoOfRooms = 4,
                        FlatNo = 3, Description = "Lovely new appartment in a building located downtown.",
                        Comodities = new List<Comodity>{ new Comodity { Description = "High quality gas", Price = 1500, Currency = Currency.EUR} } },
                    new Housing { HousingType = HousingType.Penthouse, Surface = 250, NoOfRooms = 10,
                        NoOfFlats = 2, FlatNo = 3, Description = "Luxurious penthouse with an amazing view.",
                        Comodities = new List<Comodity>{ new Comodity { Description = "Hi-tech AC", Price = 3000, Currency = Currency.EUR},
                            new Comodity { Description = "Amazing view", Price = 5000, Currency = Currency.EUR} } }
                };

                houses.ForEach(h => context.Houses.Add(h));
                context.SaveChanges();
            }
        }
    }
}
