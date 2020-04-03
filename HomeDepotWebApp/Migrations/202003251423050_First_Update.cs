namespace HomeDepotWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First_Update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        PickupDay = c.DateTime(nullable: false),
                        Days = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        ToolId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookingId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Adress = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Tools",
                c => new
                    {
                        ToolId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        DepositPrice = c.Int(nullable: false),
                        RentPrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ToolId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tools");
            DropTable("dbo.Customers");
            DropTable("dbo.Bookings");
        }
    }
}
