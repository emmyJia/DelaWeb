namespace DelaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Other1 = c.String(),
                        Other2 = c.String(),
                        Other3 = c.String(),
                        Other4 = c.String(),
                        Other5 = c.String(),
                    })
                .PrimaryKey(t => t.OrderID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        ItemCode = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Bonus = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Type = c.Int(nullable: false),
                        Other1 = c.String(),
                        Other2 = c.String(),
                        Other3 = c.String(),
                        Other4 = c.String(),
                        Other5 = c.String(),
                        Order_OrderID = c.Int(),
                    })
                .PrimaryKey(t => t.ItemID)
                .ForeignKey("dbo.Orders", t => t.Order_OrderID)
                .Index(t => t.Order_OrderID);
            
            AddColumn("dbo.Customers", "TaxID", c => c.String());
            AddColumn("dbo.Customers", "Other1", c => c.String());
            AddColumn("dbo.Customers", "Other2", c => c.String());
            AddColumn("dbo.Customers", "Other3", c => c.String());
            AddColumn("dbo.Customers", "Other4", c => c.String());
            AddColumn("dbo.Customers", "Other5", c => c.String());
            AddColumn("dbo.Invoices", "Other1", c => c.String());
            AddColumn("dbo.Invoices", "Other2", c => c.String());
            AddColumn("dbo.Invoices", "Other3", c => c.String());
            AddColumn("dbo.Invoices", "Other4", c => c.String());
            AddColumn("dbo.Invoices", "Other5", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Order_OrderID", "dbo.Orders");
            DropIndex("dbo.Products", new[] { "Order_OrderID" });
            DropColumn("dbo.Invoices", "Other5");
            DropColumn("dbo.Invoices", "Other4");
            DropColumn("dbo.Invoices", "Other3");
            DropColumn("dbo.Invoices", "Other2");
            DropColumn("dbo.Invoices", "Other1");
            DropColumn("dbo.Customers", "Other5");
            DropColumn("dbo.Customers", "Other4");
            DropColumn("dbo.Customers", "Other3");
            DropColumn("dbo.Customers", "Other2");
            DropColumn("dbo.Customers", "Other1");
            DropColumn("dbo.Customers", "TaxID");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
        }
    }
}
