namespace DistributionTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableProductDistribution : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductDistributions",
                c => new
                    {
                        PLU = c.Int(nullable: false),
                        StoreNumber = c.Int(nullable: false),
                        DistributionCover = c.Single(nullable: false),
                        StockAfterDistribution = c.Int(nullable: false),
                        DistributedQuantity = c.Int(nullable: false),
                        DistributedPacks = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PLU, t.StoreNumber });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductDistributions");
        }
    }
}
