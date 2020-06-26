namespace DistributionTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteredTabelesColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductSales", "AverageSales", c => c.Int(nullable: false));
            AddColumn("dbo.ProductStocks", "EffectiveCover", c => c.Single(nullable: false));
            DropColumn("dbo.Products", "StoresCover");
            DropColumn("dbo.ProductStocks", "Stock");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductStocks", "Stock", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "StoresCover", c => c.Single(nullable: false));
            DropColumn("dbo.ProductStocks", "EffectiveCover");
            DropColumn("dbo.ProductSales", "AverageSales");
        }
    }
}
