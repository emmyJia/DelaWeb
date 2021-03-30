namespace DelaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BonusAdding : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Address", c => c.String());
            AddColumn("dbo.OrderDetails", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.OrderDetails", "Bonus", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "Bonus");
            DropColumn("dbo.OrderDetails", "Price");
            DropColumn("dbo.Orders", "Address");
        }
    }
}
