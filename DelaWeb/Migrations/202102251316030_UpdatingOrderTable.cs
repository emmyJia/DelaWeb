namespace DelaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingOrderTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Order_OrderID", "dbo.Orders");
            DropIndex("dbo.Products", new[] { "Order_OrderID" });
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Orders", "Details_ID", c => c.Int());
            AddColumn("dbo.Products", "OrderDetails_ID", c => c.Int());
            CreateIndex("dbo.Orders", "Details_ID");
            CreateIndex("dbo.Products", "OrderDetails_ID");
            AddForeignKey("dbo.Products", "OrderDetails_ID", "dbo.OrderDetails", "ID");
            AddForeignKey("dbo.Orders", "Details_ID", "dbo.OrderDetails", "ID");
            DropColumn("dbo.Products", "Order_OrderID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Order_OrderID", c => c.Int());
            DropForeignKey("dbo.Orders", "Details_ID", "dbo.OrderDetails");
            DropForeignKey("dbo.Products", "OrderDetails_ID", "dbo.OrderDetails");
            DropIndex("dbo.Products", new[] { "OrderDetails_ID" });
            DropIndex("dbo.Orders", new[] { "Details_ID" });
            DropColumn("dbo.Products", "OrderDetails_ID");
            DropColumn("dbo.Orders", "Details_ID");
            DropTable("dbo.OrderDetails");
            CreateIndex("dbo.Products", "Order_OrderID");
            AddForeignKey("dbo.Products", "Order_OrderID", "dbo.Orders", "OrderID");
        }
    }
}
