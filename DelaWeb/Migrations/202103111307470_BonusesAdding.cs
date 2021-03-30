namespace DelaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BonusesAdding : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bonus",
                c => new
                    {
                        BonusID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        BonusTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        AddRest = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BonusID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Bonus");
        }
    }
}
