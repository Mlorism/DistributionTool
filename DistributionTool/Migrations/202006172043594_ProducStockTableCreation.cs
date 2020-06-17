namespace DistributionTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProducStockTableCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductStocks",
                c => new
                    {
                        PLU = c.Int(nullable: false),
                        StoreNumber = c.Int(nullable: false),
                        Stock = c.Int(nullable: false),
                        EffectiveStock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PLU, t.StoreNumber });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductStocks");
        }
    }
}
