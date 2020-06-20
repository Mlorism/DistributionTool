namespace DistributionTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductSalesAltered : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ProductSales");
            AddColumn("dbo.ProductSales", "SlsLW", c => c.Int(nullable: false));
            AddColumn("dbo.ProductSales", "SlsLW1", c => c.Int(nullable: false));
            AddColumn("dbo.ProductSales", "SlsLW2", c => c.Int(nullable: false));
            AddColumn("dbo.ProductSales", "SlsLW3", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ProductSales", new[] { "PLU", "StoreNumber" });
            DropColumn("dbo.ProductSales", "Date");
            DropColumn("dbo.ProductSales", "Sales");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductSales", "Sales", c => c.Int(nullable: false));
            AddColumn("dbo.ProductSales", "Date", c => c.DateTime(nullable: false));
            DropPrimaryKey("dbo.ProductSales");
            DropColumn("dbo.ProductSales", "SlsLW3");
            DropColumn("dbo.ProductSales", "SlsLW2");
            DropColumn("dbo.ProductSales", "SlsLW1");
            DropColumn("dbo.ProductSales", "SlsLW");
            AddPrimaryKey("dbo.ProductSales", new[] { "PLU", "StoreNumber", "Date" });
        }
    }
}
