namespace DistributionTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteredTabelesColumns2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "WarehouseDistributedQty", c => c.Int(nullable: false));
            AlterColumn("dbo.ProductSales", "AverageSales", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductSales", "AverageSales", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "WarehouseDistributedQty");
        }
    }
}
