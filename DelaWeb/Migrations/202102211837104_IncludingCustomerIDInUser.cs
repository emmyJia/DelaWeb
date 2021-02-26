namespace DelaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncludingCustomerIDInUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CustomerID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CustomerID");
        }
    }
}
