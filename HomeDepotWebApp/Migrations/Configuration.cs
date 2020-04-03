namespace HomeDepotWebApp.Migrations
{
    using HomeDepotWebApp.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HomeDepotWebApp.Storage.HomeDepotContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HomeDepotWebApp.Storage.HomeDepotContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Tools.AddOrUpdate(new Tool { Name = "Lan mower" , DepositPrice = 400, Description = "Get your grass cut" , RentPrice = 50});
            context.Tools.AddOrUpdate(new Tool { Name = "Chainsaw", DepositPrice = 150, Description = "Get your bushes cut", RentPrice = 40 });
            context.Tools.AddOrUpdate(new Tool { Name = "Floor Saner", DepositPrice = 500, Description = "Get your floor sanded", RentPrice = 60 });


            context.Customers.AddOrUpdate(new Customer { Name = "Hans Hansen", Email = "hans@hans.dk", Adress = "Hansvej 29", Password = "hans123"});
            context.Customers.AddOrUpdate(new Customer { Name = "Jens Jensen", Email = "jens@jens.dk", Adress = "Jensven 30", Password = "jens123" });
            context.Customers.AddOrUpdate(new Customer { Name = "Karl Karlsen", Email = "karl@karl.dk", Adress = "Karlvej 31", Password = "karl123" });
            context.Customers.AddOrUpdate(new Customer { Name = "Henrik Henriksen", Email = "henrik@henrik.dk", Adress = "Henrikvej 32", Password = "henrik123" });

            context.Bookings.AddOrUpdate(new Booking { CustomerId = 1, Days= 3, PickupDay = new DateTime(2020,4,1), ToolId = 2, Status = "Reserveret" });
            context.Bookings.AddOrUpdate(new Booking { CustomerId = 1, Days = 4, PickupDay = new DateTime(2020, 4,3), ToolId = 3, Status = "Reserveret" });
            context.Bookings.AddOrUpdate(new Booking { CustomerId = 2, Days = 7, PickupDay = new DateTime(2020, 4, 10), ToolId = 2, Status = "Reserveret" });
            context.Bookings.AddOrUpdate(new Booking { CustomerId = 3, Days = 2, PickupDay = new DateTime(2020, 4, 9), ToolId = 1, Status = "Reserveret" });

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
