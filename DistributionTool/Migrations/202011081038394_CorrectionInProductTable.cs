namespace DistributionTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectionInProductTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "WarehouseDistributedQty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "WarehouseDistributedQty", c => c.Int(nullable: false));
        }
    }
}
