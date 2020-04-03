namespace HomeDepotWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pasword_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Email", c => c.String());
            DropColumn("dbo.Customers", "Password");
        }
    }
}
